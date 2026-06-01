# PassLock

This project houses a password manager that encrypts and decrypts values provided by the user directly to the command line. It is an example of how to write clean code that communicates intention and avoids spaghetti code.

The [Program.cs](PassLock/Program.cs) file provides a high-level view of the operations supported by the password manager.

## NuGet Dependencies

- .NET 10.0 SDK
- Microsoft.EntityFrameworkCore 10.0.8
- Microsoft.EntityFrameworkCore.Sqlite 10.0.8
- Microsoft.EntityFrameworkCore.Design 10.0.8
- Microsoft.EntityFrameworkCore.Tools 10.0.8

> Note: `Microsoft.EntityFrameworkCore.Design` and `Microsoft.EntityFrameworkCore.Tools` are included for build-time support and database migrations. You only need the .NET 10 SDK to run the application.

## Usage

PassLock is a command-line password manager. It stores accounts, domains, and encrypted passwords in a local SQLite database.

### Available commands

- `account add` - Add a new account by entering email and username interactively.
- `account list` - List all saved accounts.
- `account remove <id|email|username>` - Remove an account by ID, email, or username.
- `domain add` - Add a new domain interactively.
- `domain list` - List all saved domains.
- `domain remove <id>` - Remove a domain by ID.
- `encrypt <accountId> <domainId>` - Encrypt a password for the given account and domain.
- `decrypt <accountId> <domainId>` - Decrypt the password for the account and domain and copy it to the clipboard.

### Accounts

Accounts represent the email/user identity used to sign in to a service. Each account has:

- `Id` - numeric database identifier
- `Email` - sign-in email address
- `Username` - optional user name

### Domains

Domains represent the website or application that requires a password. Each domain has:

- `Id` - numeric database identifier
- `Name` - domain or application name

### Passwords

Passwords are encrypted with AES and stored in the database. Passwords are linked to an account and a domain using the `AccountPasswordForDomain` association.

### Encryption

To store a password for a specific account and domain, run:

```zsh
dotnet run --project PassLock/PassLock.csproj encrypt <accountId> <domainId>
```

The tool will prompt you to enter the password to encrypt. Once saved, the password is associated with the specified account and domain.

### Decryption

To retrieve and decrypt a password for a specific account and domain, run:

```zsh
dotnet run --project PassLock/PassLock.csproj decrypt <accountId> <domainId>
```

The decrypted password is copied to the clipboard on macOS.

### Examples

```zsh
# Add an account
dotnet run --project PassLock/PassLock.csproj account add

# List accounts
dotnet run --project PassLock/PassLock.csproj account list

# Add a domain
dotnet run --project PassLock/PassLock.csproj domain add

# List domains
dotnet run --project PassLock/PassLock.csproj domain list

# Encrypt and save a password for account 1 and domain 2
dotnet run --project PassLock/PassLock.csproj encrypt 1 2

# Decrypt the password for account 1 and domain 2
dotnet run --project PassLock/PassLock.csproj decrypt 1 2
```
