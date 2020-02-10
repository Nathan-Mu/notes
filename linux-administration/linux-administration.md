# Linux Administration

## 1. Linux Fundamentals

### 1.1 The Linux Directory Structure

#### Common Directories

| path  | meaning                                      |
| ----- | -------------------------------------------- |
| /     | "Root", the top of the file system hierarchy |
| /bin  | Binaries and other executable programs       |
| /etc  | System configuration files                   |
| /home | Home directories                             |
| /opt  | Optional or third party software             |
| /tmp  | Temporary space, typically cleared on reboot |
| /usr  | User related programs                        |
| /var  | Variable data, most notably log files        |

#### The prompt

Super user (#)

```bash
[root@linuxsvr ~]#
```

Normal user($)

```bash
[jason@linuxsvr ~]$
```

Home directory(~)

- ~jason = /home/jason
- ~pat = /home/pat

### 1.2 Basic Linux Commands

| command | meaning                                 |
| ------- | --------------------------------------- |
| ls      | Lists directory contents                |
| cd      | Changes the current directory           |
| pwd     | Displays the present working directory  |
| cat     | Concatenates and displays files         |
| echo    | Displays arguments to the screen        |
| man     | Displays the online manual              |
| exit    | Exits the shell or your current session |
| clear   | Clears the screen                       |

### 1.3 Getting Help at the Command

#### Navigating man pages

| Action       | Meaning                        |
| ------------ | ------------------------------ |
| enter        | Move down one line             |
| space        | Move down one page             |
| g            | Move to the top of the page    |
| G or shift+g | Move to the bottom of the page |
| q            | Quit                           |

#### Environmental Variables

- Storage location that has a name and a value
- Typically uppercase
- Access the contents by executing:
  - echo $VAR_NAME

####  PATH

- An environment variable
- Controls the command search path
- Contains a list of directories

#### which command

- Locate a command

```bash
$ which cat
/bin/cat
```

#### Get Help

- Add --help or -h to a command to get help

####  Searching Man Page

- man -k SEARCH_TERM

```bash
$ man -k calendar
cal (1) - displays a calendar
difftime (3p) - xxxxxxxxxxxxxxxxxxxxxx
```

