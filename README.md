# _Band Tracker_

#### _App for showing bands that have played at a certain venue and vice versa, 02/24/2017_

#### By _**Elise St Hilaire**_

## Description

_This program tracks bands and the venues where they've played._

## Setup/Installation Requirements

_Create database_
In SQLMD:
* _> CREATE DATABASE band_tracker;_
* _> GO_
* _> USE band_tracker;_
* _> GO_
* _> CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));_
* _> GO_
* _> CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));_
* _> GO_
* _> CREATE TABLE bands_venues (band_id INT, venue_id INT);_
* _> GO_


## Specs

**The program returns a list of saved bands.**  
* Input: "Journey", "Kansas", "38 Special";
* Output: "Journey", "Kansas", "38 Special";

**The program returns a list of saved venues.**  
* Input: "The Triple Door", "The Showbox", "The Crocodile";
* Output: "The Triple Door", "The Showbox", "The Crocodile";

**The program returns a list of venues where a band has performed.**  
* Input: "Journey" *click*  
* Output:  "The Triple Door", "The Showbox", "The Crocodile";

**The program returns a list of bands that have played at a given venue.**  
* Input: "The Showbox" *click*  
* Output: Name: "Journey", "Kansas", "38 Special";

**The program allows to update a venue's details.**  
* Input: Change "The Crawcodile" to "The Crocodile"
* Output: Name: Recorded updated! Venue's new name is "The Crocodile"

**The program allows to delete a venue.**  
* Input: Delete "The Showbox"
* Output: Showbox deleted.


## Known Bugs

_Not that I know of._

## Support and contact details

_Please contact me with any problems at eliseylenore@gmail.com_

## Technologies Used

_I used C# with the Nancy framework and Razor. I used SQL to create the database. I also relied on Bootstrap._

### License

*MIT*

Copyright (c) 2016 **_Elise St Hilaire_**
