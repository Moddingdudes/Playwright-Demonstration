# Playwright-Demonstration
A simple demonstration repo for using Playwright for testing a website.

This was written to showcase how I would implement a simple Playwright QA automation test for my [test website](https://playwright-test-website.saltedpepper21.repl.co/)

This test ensures the navbar is visible, the navbar badge routes you to home, and the home page, sign up page, and login page test buttons all properly execute.

This simple test framework uses POM's and abstract classes to comply with DRY, primarily for the navbar which is static across all sites, so testing is the same.
