# Tabby   <img src="https://github.com/confused-Techie/Tabby/blob/master/Tabby_Docker/wwwroot/Images/tabbyIcon.svg" alt="Tabby Icon Logo" width=5% />
<br/>
<p align=center>
<a href="https://github.com/confused-Techie/Tabby/blob/master/LICENSE">
   <img alt="Tabby GPL 3.0 License" src="https://img.shields.io/badge/license-GPL--3.0-orange">
</a>
<a href="https://github.com/confused-Techie/Tabby/releases/tag/v1.0">
   <img alt="Tabby Release Version" src="https://img.shields.io/badge/release-v1.0-blueviolet">
</a>
<a href="https://hub.docker.com/r/lhbasics/tabbydocker">
   <img alt="Tabby Docker Pulls" src="https://img.shields.io/docker/pulls/lhbasics/tabbydocker.svg">
</a>
<br/>
<a href="https://addons.mozilla.org/en-US/firefox/addon/tabby-extension/">
   <img alt="Tabby Extension for Firefox" src="https://img.shields.io/badge/Extension-Firefox-orange">
</a>
<a href="https://chrome.google.com/webstore/detail/tabby-chrome-extension/ifjdlkpicmjmojpfnnegehnkblbjhnad?hl=en">
   <img alt="Tabby Extension for Google Chrome" src="https://img.shields.io/badge/Extension-Chrome-red">
</a>
<a href="https://microsoftedge.microsoft.com/addons/detail/tabby-edge-extension/obbjajbinlombefaffnlmmkapbendfmn">
   <img alt="Tabby Extension for Microsoft Edge" src="https://img.shields.io/badge/Extension-Edge-blue">
</a>
<a href="https://addons.opera.com/en/extensions/details/tabby-extension/">
   <img alt="Tabby Extension for Opera addons" src="https://img.shields.io/badge/Extension-Opera-red">
</a>
</p>
Tabby is the Self-Hosted Free Bookmark Manager. A stark contrast to keeping a list of Bookmarks as just URL's and Titles, Tabby lets you provide just a URL via one of the Browser Extensions which Tabby then adds the Title according to the website, a description, and Site Name if the website supports it.

Tabby is completly local, only contacting the website once when added, and Google for the favicons. The extensions themselves are also completely local, only contacting your server once you choose to save a Bookmark.
<img src="https://github.com/confused-Techie/Tabby/blob/master/gitImages/HomePage.PNG" alt="Tabby Home Page" />

Tabby is still in development and if you encounter any issues feel free to open an issue here on Github, or if you have any feature requests feel free to submit those as well.

# Extensions
<strong>Github</strong><br />

<a href="https://github.com/confused-Techie/TabbyChromeExtension">Github Chromium Based Extension</a>

<a href="https://github.com/confused-Techie/TabbyFirefoxExtension">Github Firefox Extension</a>

<strong>Published Versions</strong><br />

Chrome Web Store: Version 1.0 Here!

<a href="https://chrome.google.com/webstore/detail/tabby-chrome-extension/ifjdlkpicmjmojpfnnegehnkblbjhnad?hl=en"><img src="https://github.com/confused-Techie/Tabby/blob/master/gitImages/chrome-addon.png" alt="Get Chrome Web Store Button" width=20% /></a>

Firefox Add-ons: Version 1.0 Here!

<a href="https://addons.mozilla.org/en-US/firefox/addon/tabby-extension/"><img src="https://github.com/confused-Techie/Tabby/blob/master/gitImages/firefox-addon.png" alt="Get Firefox Add-On Button" /></a>

Opera addons: Version 1.0.1 Here!

<a href="https://addons.opera.com/en/extensions/details/tabby-extension/"><img src="https://github.com/confused-Techie/Tabby/blob/master/gitImages/opera-addon.png" alt="Get Opera addons Button" width=20% /></a>

Edge Add-ons Beta: Version 1.0 Here!

<a href="https://microsoftedge.microsoft.com/addons/detail/tabby-edge-extension/obbjajbinlombefaffnlmmkapbendfmn"><img src="https://github.com/confused-Techie/Tabby/blob/master/gitImages/microsoftStore-addon.png" alt="Get from Microsoft Edge Add-On Beta Button" width=20% /></a>

# Installation

<strong>Windows:</strong><br />
Make sure to first install <a href="https://docs.docker.com/docker-for-windows/install/">Docker Desktop.</a>

Once installed make sure it's using Linux Containers.

Then in any directory create a new file titled `docker-compose.yml`

Open the file and type in the following.

```bash
version: '3'
services:
   ms-sql-server:
      image: mcr.microsoft.com/mssql/server
      environment:
         ACCEPT_EULA: "Y"
         SA_PASSWORD: "Pa55w0rd2020"        # Make sure this matches the password specified below
         MSSQL_PID: Express
      ports:
         - "1433:1433"
   web:
      image: "lhbasics/tabbydocker:latest"
      ports:
         - "8080:80"
      environment:                        # The only values required are the DBServer, and the DBPassword
         DBServer: "ms-sql-server"        # All other values will default to whats listed here, 
         DBUser: "SA"                     # so unless changing other values these can be left alone.
         DBPassword: "Pa55w0rd2020"
         DBPort: "1433"
         Database: "Bookmarks"
```  
Lastly open this file in PowerShell. And type the following...
```bash
docker-compose up
```
Now the Server is running and you can connect in your web browser with `http://localhost:8080` Or whatever port specified under `web` in `docker-compose.yml`.

<strong>Linux</strong><br />
Make sure to install <a href="https://docs.docker.com/engine/install/">Docker</a> and <a href="https://docs.docker.com/compose/install/">Docker Compose</a> based on your distribution.

Then just like the Windows Installation create a file `docker-compose.yml`, and open the file.
```bash
touch docker-compose.yml
nano docker-compose.yml
```
Then inside the `docker-compose.yml` type the following.

```bash
version: '3'
services:
   ms-sql-server:
      image: mcr.microsoft.com/mssql/server
      environment:
         ACCEPT_EULA: "Y"
         SA_PASSWORD: "Pa55w0rd2020"        # Make sure this matches the password specified below
         MSSQL_PID: Express
      ports:
         - "1433:1433"
   web:
      image: "lhbasics/tabbydocker:latest"
      ports:
         - "8080:80"
      environment:                        # The only values required are the DBServer, and the DBPassword
         DBServer: "ms-sql-server"        # All other values will default to whats listed here, 
         DBUser: "SA"                     # so unless changing other values these can be left alone.
         DBPassword: "Pa55w0rd2020"
         DBPort: "1433"
         Database: "Bookmarks"
```  

Save the file and type...
```bash
docker-compose up
```

And Tabby is now running, connect to it with the port specified in the web definition via your prefered browser.
