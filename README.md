# PhotoPortal
PhotoPortal is an web application designed for photographers. 
Users have the ability to view other users photos and clubs. Once logged in, users can create and edit their own clubs and upload photos. 

This appliaction was made using modified BootstrapMade template [PhotoFolio](https://bootstrapmade.com/photofolio-bootstrap-photography-website-template/).

#### TODO
- [ ] Users have ability to join clubs
- [ ] User can rate other users' photos
- [ ] Photo filtering based on photo categories, upload date, etc.

### How to run this project 🖥️
1. Git clone this repository 
```
  $ git clone https://github.com/TomasPecinka/PhotoPortal.git
```
2. Insert your database connection string to [appsettings.json](appsettings.json)
3. Seed database by running this command
```
  $ dotnet run seeddata
```
4. Add [Cloudinary](https://cloudinary.com) `CloudName`, `ApiKey` and `ApiSecret` to [appsettings.json](appsettings.json)
