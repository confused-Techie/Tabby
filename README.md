# Tabby
Tabby is the Self Hosted Solution for your Bookmarks, allowing you to save a page easily in the browser, and view it later, grouped with anything else you've saved. 
Seeing not only the link of the website, but using the Open Graph protocl and Twitter Cards to make your Bookmarks make sense at a glance. 
Being able to see Descriptions, website names, and the time you saved the bookmark quickly.<br /><br />
While usable this project is definetly far from done, and I hope to continually make improvements, and feel free to request new features.<br /><br />
<img src="https://github.com/confused-Techie/Tabby/blob/master/gitImages/HomePage.PNG" alt="Tabby Home Page" />
<strong>Installation</strong><br />
Installing is easy using Docker and Docker Compose.<br />
Simply create your `docker-compose.yml` and enter the following.<br />
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
Afterwards save the file and use<br />
```bash
docker-compose up
```
While in the same folder, and connect using the proper ports specified in the compose file.<br />
Afterwards you can install the browser extension to start saving pages.<br /><br />
<strong>Extensions</strong><br />
Currently the only supported extension is for Chromium Based Browsers, which you can learn more about <a href="https://github.com/confused-Techie/TabbyChromeExtension"here.</a>
<br />
As of now the extension is the version submitted to the Chrome Web Store. But will work on Opera, Edge, Brave without any changes. But a modified manifest file is being submitted to each of those services only to display the proper meta data. And an extension for Firefox is currently in the works.
