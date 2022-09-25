# Jay Test Suite -- README #

This README would normally document whatever steps are necessary to get your application up and running.

### Quick Links ###
* [JaySharp Testing Docs](#jaySharp_testing_docs)
* [JaySharp CLI Commands](#jaySharp_cli_commands)
* [Git CLI Commands](#git_cli_commands)
* [DotNet CLI Commands](#dotnet_cli_commands)
* [EF Migrations CLI Commands](#ef_migrations_cli_commands)
* [Sql Lite](#sql_lite_commands)

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions


### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact

### DotNet CLI Commands ###
* dotnet run   --runs a project
* dotnet clean  --cleans a project
* dotnet build  --builds a project (there are options for verbosity like -v d[etailed])
* dotnet watch run --runs a project while also listening for changes to files (can edit code while in a persistant run mode)
<a name="dotnet_cli_commands"></a>

### Git CLI Commands ###
* stashing
    - apply stash@{stash_index}
    - git stash apply (shortcut for git stash apply stash@{0})
    - git stash apply stash@{0} /*apply most 
<a name="git_cli_commands"></a>

# Testing Documentation #
<a name="jaySharp_testing_docs"></a>
## Abstracts ##
---
### Base Extensions ###
- OughtTo
- Must
### Base Modifiers ###
- Be
- Exist
- Occur
- Wait
- Persist

# JaySharp -- CLI Commands #
* dotnet run -- JaySharp 
    - base for all JaySharp cli commands
* dotnet run -- JaySharp version
    - returns version of JaySharp
* dotnet run -- JayShard RunTests
    - runs all the tests/suites tagged with JaySharp
<a name="jaySharp_cli_commands"></a>