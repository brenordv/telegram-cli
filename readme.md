# Telegram CLI
This is a simple project to send messages to a Telegram chat using a CLI.

The motivation was: I hate waiting for long processes to run, so I can pipe the result of the command to my telegram 
with this CLI and get notified when it's done, having a way to glance at the result. Meanwhile, I can do other things. 

If the message is bigger than what is allowed by Telegram (4096), it will be split into multiple messages.


# Pre-requisites
To work, we need two environment variables: `TELEGRAM_API_TOKEN` and `TELEGRAM_CHAT_ID`.

**Setting Environment Variables on Windows**

1. Search for 'Environment Variables' in your Start Menu.
2. Click on 'Edit the system environment variables.'
3. Click on 'Environment Variables' at the bottom right corner.
4. Under 'System Variables', click on the 'New' button.
5. In the 'Variable name' field, enter `TELEGRAM_API_TOKEN` and in the 'Variable value' field, enter your telegram API token. Click 'OK'.
6. Repeat the process for the `TELEGRAM_CHAT_ID` variable.

These changes will take effect the next time you open a command prompt.

**Setting Environment Variables on Linux**

1. Open your terminal.
2. Use the `export` command to add your environment variables. Replace `your_telegram_api_token` and `your_telegram_chat_id` with your actual values:
    ```bash
    export TELEGRAM_API_TOKEN=your_telegram_api_token
    export TELEGRAM_CHAT_ID=your_telegram_chat_id
    ```
3. To make these changes permanent, add these lines to your `~/.bashrc`, `~/.bash_profile`, or `~/.profile` file, and then source it:
    ```bash
    echo 'export TELEGRAM_API_TOKEN=your_telegram_api_token' >> ~/.bashrc
    echo 'export TELEGRAM_CHAT_ID=your_telegram_chat_id' >> ~/.bashrc
    source ~/.bashrc
    ```
**Setting Environment Variables on MacOS**

1. Open your terminal.
2. Use the `export` command to add your environment variables. Replace `your_telegram_api_token` and `your_telegram_chat_id` with your actual values:
    ```bash
    export TELEGRAM_API_TOKEN=your_telegram_api_token
    export TELEGRAM_CHAT_ID=your_telegram_chat_id
    ```
3. To make these changes permanent, add these lines to your `~/.bash_profile` or `~/.zshrc` file (depending on your default shell), and then source it:
    ```bash
    echo 'export TELEGRAM_API_TOKEN=your_telegram_api_token' >> ~/.bash_profile
    echo 'export TELEGRAM_CHAT_ID=your_telegram_chat_id' >> ~/.bash_profile
    source ~/.bash_profile
    ```
For `zsh`:
```bash
echo 'export TELEGRAM_API_TOKEN=your_telegram_api_token' >> ~/.zshrc
echo 'export TELEGRAM_CHAT_ID=your_telegram_chat_id' >> ~/.zshrc
source ~/.zshrc
```

Please replace `your_telegram_api_token` and `your_telegram_chat_id` with your actual values. The commands should be run without the angle brackets.

# How to use

## Multiple arguments
```shell
tme.exe Hello World
```
Will send:
> Hello World
 
## One argument
```shell
tme.exe "Hello World"
```
Will send:
> Hello World

## Piped data
```shell
dir | tme.exe 
```
Will send:
```
 Volume in drive C is vault
 Volume Serial Number is FX42-6667

 Directory of C:\path\to\this\project\Telegram.Cli\Telegram.Cli\bin\Debug\net6.0

23/05/2023  22:59    <DIR>          .
23/05/2023  22:59    <DIR>          ..
23/05/2023  22:59               401 tme.deps.json
23/05/2023  23:03            14.336 tme.dll
23/05/2023  23:03           148.992 tme.exe
23/05/2023  23:03            12.660 tme.pdb
23/05/2023  22:59               147 tme.runtimeconfig.json
             5 File(s)          176.536 bytes
             2 Dir(s) 9.889.064.771.584 bytes free
```
