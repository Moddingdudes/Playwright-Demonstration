"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.kBrowserCloseMessageId = exports.FFSession = exports.FFConnection = exports.ConnectionEvents = void 0;
var _events = require("events");
var _stackTrace = require("../../utils/stackTrace");
var _debugLogger = require("../../common/debugLogger");
var _helper = require("../helper");
var _protocolError = require("../protocolError");
/**
 * Copyright 2017 Google Inc. All rights reserved.
 * Modifications copyright (c) Microsoft Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the 'License');
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an 'AS IS' BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

const ConnectionEvents = {
  Disconnected: Symbol('Disconnected')
};

// FFPlaywright uses this special id to issue Browser.close command which we
// should ignore.
exports.ConnectionEvents = ConnectionEvents;
const kBrowserCloseMessageId = -9999;
exports.kBrowserCloseMessageId = kBrowserCloseMessageId;
class FFConnection extends _events.EventEmitter {
  constructor(transport, protocolLogger, browserLogsCollector) {
    super();
    this._lastId = void 0;
    this._transport = void 0;
    this._protocolLogger = void 0;
    this._browserLogsCollector = void 0;
    this._browserDisconnectedLogs = void 0;
    this.rootSession = void 0;
    this._sessions = void 0;
    this._closed = void 0;
    this.setMaxListeners(0);
    this._transport = transport;
    this._protocolLogger = protocolLogger;
    this._browserLogsCollector = browserLogsCollector;
    this._lastId = 0;
    this._sessions = new Map();
    this._closed = false;
    this.rootSession = new FFSession(this, '', message => this._rawSend(message));
    this._sessions.set('', this.rootSession);
    this._transport.onmessage = this._onMessage.bind(this);
    // onclose should be set last, since it can be immediately called.
    this._transport.onclose = this._onClose.bind(this);
  }
  nextMessageId() {
    return ++this._lastId;
  }
  _rawSend(message) {
    this._protocolLogger('send', message);
    this._transport.send(message);
  }
  async _onMessage(message) {
    this._protocolLogger('receive', message);
    if (message.id === kBrowserCloseMessageId) return;
    const session = this._sessions.get(message.sessionId || '');
    if (session) session.dispatchMessage(message);
  }
  _onClose() {
    this._closed = true;
    this._transport.onmessage = undefined;
    this._transport.onclose = undefined;
    this._browserDisconnectedLogs = _helper.helper.formatBrowserLogs(this._browserLogsCollector.recentLogs());
    this.rootSession.dispose();
    Promise.resolve().then(() => this.emit(ConnectionEvents.Disconnected));
  }
  close() {
    if (!this._closed) this._transport.close();
  }
  createSession(sessionId) {
    const session = new FFSession(this, sessionId, message => this._rawSend({
      ...message,
      sessionId
    }));
    this._sessions.set(sessionId, session);
    return session;
  }
}
exports.FFConnection = FFConnection;
class FFSession extends _events.EventEmitter {
  constructor(connection, sessionId, rawSend) {
    super();
    this._connection = void 0;
    this._disposed = false;
    this._callbacks = void 0;
    this._sessionId = void 0;
    this._rawSend = void 0;
    this._crashed = false;
    this.on = void 0;
    this.addListener = void 0;
    this.off = void 0;
    this.removeListener = void 0;
    this.once = void 0;
    this.setMaxListeners(0);
    this._callbacks = new Map();
    this._connection = connection;
    this._sessionId = sessionId;
    this._rawSend = rawSend;
    this.on = super.on;
    this.addListener = super.addListener;
    this.off = super.removeListener;
    this.removeListener = super.removeListener;
    this.once = super.once;
  }
  markAsCrashed() {
    this._crashed = true;
  }
  _closedErrorMessage() {
    if (this._crashed) return 'Target crashed';
    if (this._connection._browserDisconnectedLogs !== undefined) return `Browser closed.` + this._connection._browserDisconnectedLogs;
    if (this._disposed) return `Target closed`;
    if (this._connection._closed) return 'Browser closed';
  }
  async send(method, params) {
    const closedErrorMessage = this._closedErrorMessage();
    if (closedErrorMessage) throw new _protocolError.ProtocolError(true, closedErrorMessage);
    const id = this._connection.nextMessageId();
    this._rawSend({
      method,
      params,
      id
    });
    return new Promise((resolve, reject) => {
      this._callbacks.set(id, {
        resolve,
        reject,
        error: new _protocolError.ProtocolError(false),
        method
      });
    });
  }
  sendMayFail(method, params) {
    return this.send(method, params).catch(error => _debugLogger.debugLogger.log('error', error));
  }
  dispatchMessage(object) {
    if (object.id) {
      const callback = this._callbacks.get(object.id);
      // Callbacks could be all rejected if someone has called `.dispose()`.
      if (callback) {
        this._callbacks.delete(object.id);
        if (object.error) callback.reject(createProtocolError(callback.error, callback.method, object.error));else callback.resolve(object.result);
      }
    } else {
      Promise.resolve().then(() => this.emit(object.method, object.params));
    }
  }
  dispose() {
    this._disposed = true;
    this._connection._sessions.delete(this._sessionId);
    const errorMessage = this._closedErrorMessage();
    for (const callback of this._callbacks.values()) {
      callback.error.sessionClosed = true;
      callback.reject((0, _stackTrace.rewriteErrorMessage)(callback.error, errorMessage));
    }
    this._callbacks.clear();
  }
}
exports.FFSession = FFSession;
function createProtocolError(error, method, protocolError) {
  let message = `Protocol error (${method}): ${protocolError.message}`;
  if ('data' in protocolError) message += ` ${protocolError.data}`;
  return (0, _stackTrace.rewriteErrorMessage)(error, message);
}