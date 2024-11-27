# User Management System with Secure CAPTCHA and Email Functionality

This project is a **User Management System** built using ASP.NET Web Forms. It provides functionalities for managing user accounts and incorporates a secure CAPTCHA mechanism to prevent automated submissions and enhance security. Additionally, it includes email functionality to send notifications or confirmations.

## Features

### User Management
- **Add Users**: Create new user accounts with all necessary details.
- **Update Users**: Edit user information and keep the database up-to-date.
- **Delete Users**: Remove user accounts from the system.
- **View Users**: Display a list of all registered users with sorting and filtering options.

### CAPTCHA Functionality
- **Dynamic CAPTCHA Generation**: Random alphanumeric strings are generated dynamically for each session.
- **Non-Copyable CAPTCHA**: 
  - Text selection is disabled to prevent copying.
  - Right-click and context menu actions are blocked for the CAPTCHA label.
- **Session-Based Validation**: The CAPTCHA value is securely stored in the session to ensure accurate validation.
- **User Feedback**: Provides feedback on correct or incorrect CAPTCHA entries.

### Email Functionality
- **Automated Emails**: Send email notifications to users upon successful registration or other actions.
- **SMTP Integration**: Configured to send emails using a secure SMTP server.
- **Customizable Email Templates**: Easily modify the email content to suit the application's requirements.
- **Error Handling**: Handles email failures gracefully and provides logs for debugging.

### Security Enhancements
- **Session Management**: Ensures secure storage of sensitive data like CAPTCHA values.
- **Prevention Against Bots**: Blocks automated form submissions using the CAPTCHA mechanism.
- **Customization Options**: Easily update the CAPTCHA style and behavior through code and configuration.

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/Harsh76200/user-management-captcha.git
