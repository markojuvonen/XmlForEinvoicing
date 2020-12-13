# XmlForEinvoicing

- C# Application to fetch data from a database and create an XML file for e-invoicing purposes

- Used Entity Framework to scaffold database models

- Used Linq to make queries to the database

Functionalities:
- Create a XML file, read the necessary values from database, populate the XML with them and save it to a folder

- Find the corresponding PDF file (if it exists) for the invoice, copy and paste it to the same folder with the XML

- Compress the folder containing XML and PDF into a ZIP file 

- Send the ZIP file to SFTP server

Used programs:
- Visual Studio 2019
- Microsoft SQL Server Management Studio 18
