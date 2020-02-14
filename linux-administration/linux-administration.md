# Linux Administration

## 1. Linux Fundamentals

### 1.1 The Linux Directory Structure

#### Common Directories

| path    | meaning                                      |
| ------- | -------------------------------------------- |
| `/`     | "Root", the top of the file system hierarchy |
| `/bin`  | Binaries and other executable programs       |
| `/etc`  | System configuration files                   |
| `/home` | Home directories                             |
| `/opt`  | Optional or third party software             |
| `/tmp`  | Temporary space, typically cleared on reboot |
| `/usr`  | User related programs                        |
| `/var`  | Variable data, most notably log files        |

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
| `ls`    | Lists directory contents                |
| `cd`    | Changes the current directory           |
| `pwd`   | Displays the present working directory  |
| `cat`   | Concatenates and displays files         |
| `echo`  | Displays arguments to the screen        |
| `man`   | Displays the online manual              |
| `exit`  | Exits the shell or your current session |
| `clear` | Clears the screen                       |

### 1.3 Getting Help at the Command

#### Navigating man pages

| Action                 | Meaning                        |
| ---------------------- | ------------------------------ |
| press `enter`          | Move down one line             |
| press `space`          | Move down one page             |
| press `g`              | Move to the top of the page    |
| press `G` or `shift+g` | Move to the bottom of the page |
| press `q`              | Quit                           |

#### Environmental Variables

- Storage location that has a name and a value
- Typically uppercase
- Access the contents by executing:
  - `echo $VAR_NAME`

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

- Add `--help` or `-h` to a command to get help

####  Searching Man Page

- `man -k SEARCH_TERM`

```bash
$ man -k calendar
cal (1) - displays a calendar
difftime (3p) - xxxxxxxxxxxxxxxxxxxxxx
```

### 1.4 Working with directories

#### Directory shortcuts

| shortcut | meaning                          |
| -------- | -------------------------------- |
| `.`      | This directory                   |
| `.. `    | The parent directory             |
| `cd - `  | Change to the previous directory |

#### Directory separator(/)

#### Create and remove directories

**create a directory**

```bash
$ mkdir [-p] directory_name
```

If creating a directory structure, `-p` is required. e.g. `mkdir -p 1/2/3`

**Remove a directory**

```bash
$ rmdir [-p] directory_name
```

only using for remove empty directories

**Recursively removes directory**

```bash
$ rm -rf directory_name
```

### 1.5 LS command

#### ls -l output

```bash
$ ls -l
 -rw-rw-r-- 1 jason users 10400 Sep 28 09:29 sales.data
```

| part           | meaning                     |
| -------------- | --------------------------- |
| `-rw-rw-r--`   | Permissions                 |
| `1`            | Number of Links             |
| `jason`        | Owner name                  |
| `users`        | Group name                  |
| `10400`        | Number of bytes in the file |
| `Sep 28 09:29` | Last modification time      |
| `sales.data`   | File name                   |

#### Listing all files, including hidden files

```bash
$ ls -a
```

Command options can be combined

`ls -l -a` = `ls -la`

#### Listing files by type

User `ls -F` to reveal file types

`/` means Directory

`@` means Link

`*` means Executable

#### Listing files by time and in reverse

| command    | meaning                                                   |
| ---------- | --------------------------------------------------------- |
| `ls -t`    | List files by time                                        |
| `ls -r`    | Reverse order                                             |
| `ls -latr` | Long listing including all files & reverse sorted by time |

#### Listing Files Recursively

| command   | meaning                |
| --------- | ---------------------- |
| `ls -R`   | List files recursively |
| `tree -d` | List directories only  |
| `tree -C` | Colorize output        |

#### List directories, not contents

`ls -d`

#### List files with color

`ls --color`

### 1.6 File and directory permissions

`-rw-rw-r--`

| Symbol (first character) | Type          |
| ------------------------ | ------------- |
| -                        | Regular file  |
| d                        | Directory     |
| \|                       | Symbolic link |

| Symbol | Permission |
| ------ | ---------- |
| r      | Read       |
| w      | Write      |
| x      | Execute    |

#### Permissions - File vs Directories

| Permission | File                           | Directory                                          |
| ---------- | ------------------------------ | -------------------------------------------------- |
| Read(r)    | Allows a file to be read       | Allows file name in the directory to be read       |
| Write(w)   | Allows a file to be modified   | Allows entries to be modified with the directory   |
| Execute(x) | Allows the execution of a file | Allows access to contents and metadata for entries |

#### Permission Categories

| Symbol | Category |
| ------ | -------- |
| u      | User     |
| g      | Group    |
| o      | Other    |
| a      | All      |

#### Groups

- Every user is in at least one group
- User can belong to many groups
- Groups are used to organize users
- The groups command displays a user's groups
- `id -Gn`

#### Permissions

`-rw-r--r--`

| Type | User | Group | Other |
| ---- | ---- | ----- | ----- |
| -    | rw-  | r--   | r--   |

#### Changing Permissions

| Item    | Meaning                                 |
| ------- | --------------------------------------- |
| `chmod` | Change mode command                     |
| `ugoa`  | User category (user, group, other, all) |
| `+-=`   | Add, subtract, or set permissions       |
| `rwx`   | Read, write, execute                    |

**Example**

```bash
$ ls -l sales.data
-rw-r--r-- 1 jason jason 36 Feb 6 15:30 sales.data

$ chmod g+w sales.data
$ ls -l sales.data
-rw-rw-r-- 1 jason jason 36 Feb 6 15:30 sales.data

$ chmod g-w sales.data
$ls -l sales.data
-rw-r--r-- 1 jason jason 36 Feb 6 15:30 sales.data

$ chmod u+rwx,g+wx sales.data
-rwxrwxr-- 1 jason jason 36 Feb 6 15:30 sales.data

$ chmod a=r sales.data
-r--r--r-- 1 jason jason 36 Feb 6 15:30 sales.data

$ chmod u=rwx,g=rx,o= sales.data
$ ls -l sales.data
-rwxr-x-- 1 jason jason 36 Feb 6 15:30 sales.data
```

#### Numeric based permissions

|  r   |  w   |  x   |                      |
| :--: | :--: | :--: | -------------------- |
|  0   |  0   |  0   | Value for off        |
|  1   |  1   |  1   | Binary value for on  |
|  4   |  2   |  1   | Base 10 value for on |

| Octal | Binary | String |
| ----- | ------ | ------ |
| 0     | 000    | `---`  |
| 1     | 001    | `--x`  |
| 2     | 010    | `-w-`  |
| 3     | 011    | `-wx`  |
| 4     | 100    | `r--`  |
| 5     | 101    | `r-x`  |
| 6     | 110    | `rw-`  |
| 7     | 111    | `rwx`  |

E.g. `chmod 754 sales.data`

|          |  u   |  g   |  o   |
| -------- | :--: | :--: | :--: |
| Symbolic | rwx  | r-x  | r--  |
| Binary   | 111  | 101  | 100  |
| Decimal  |  7   |  5   |  4   |

#### Change group (chgrp)

`chgrp group_name file_name`

e.g.

```bash
$ chgrp sales records.txt
```

#### The umask Command

`umask [-S] [mode]`

- Sets the file creation mask to mode, if given
- Use -S to for symbolic notation

|                      | Directory | File |
| -------------------- | --------- | ---- |
| Base Permission      | 777       | 666  |
| Subtract Umask       | -022      | -022 |
| Creations Permission | 755       | 644  |

```bash
$ umask 
0022
$ umask -S
u=rwx, g=rx, o=rx
$ umask 007 ## set file creation mask to 007
```

### 1.7 Finding files and directories

#### The find command

`find [path...] [expression]`

Recursively find files in path that match expression. If no arguments are supplied, it find all files in the current directory. 

#### find options

| options             | meaning                                          |
| ------------------- | ------------------------------------------------ |
| `-name {pattern}`   | Find files and directories that match `pattern`  |
| `-iname {pattern}`  | Like `-name`, but ignore case                    |
| `-ls`               | Performs an `ls` on each of the found items      |
| `-mtime {days}`     | Find files that are `days` old                   |
| `-size {num}`       | Find file that are of size `num`                 |
| `-newer {file}`     | Find files that are new than `file`              |
| -exec command {} \; | Run command against all the files that are found |













