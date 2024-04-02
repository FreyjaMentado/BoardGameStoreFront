# Index
Sell = Purchase thingie and then pick up in store. Will need to present ID when picking up
Manage = Read and Write

Each Details Page will have a Permission check if account is Admin
	Show Tags (Desert pack collection, ocean collection) (May or may not show by default)
	Set Price
	Set Sale % or flat?
	Edit Picture
	Edit detail info
	Edit Stock
	Edit Anything else 
# Capabilities
Sell Board Games (Also through POS )
Get Board Game Data (Input through Different App - BOS? Vendors? ConsoleApp? -> Into POS)

Renting Board Games (Different games than above. Also POS)

Get Card Data (Input through Different App - Console. Also through some update api? )
Sell Cards (Also through POS
Check Card Value (Use ScryFall?/TCG Player)

Get Sealed Product Data (Input through Different App - Console. Also through some update api? )
Sell Sealed Product  (Also through POS)
Check Sealed Product Value (Use ScryFall?/TCG Player)

Sell Other Junk (Game Accessories. Also POS)

User Accounts (Third party)
User (Library) Cards 
	Can scan to add order to their Order history
	In-store credits 
User Account In-store credit (Rewards program?)
Wishlist for users for anything we sell
Email user when thing they Wishlist is on **Sale**

Employee Info (Name, Availability)

Manage Calendar for Employee Work Schedule
Manage Calendar for Reserving Tables
Manage Calendar for upcoming events

### later
Social media links banner
Gift cards


# Customer Site Pages
**Home Page** 
	Show Event Calendar
	**Search bar at top**

**Board Game To Sell**
	**List Page**
		Quick View Modal
			Add to cart
			tiny description
			price
			Wishlist Button
	**Details Page** 
		Add to cart
		Wishlist Button

Board Game to Borrow
	List Page
		For games that we also sell, there should be some sort of marker or identifier that marks the game as purchasable on the site
		can also filter board games to games we also sell

Sealed Products To Sell
	Request Page
		User enters info that shop has never carried before and says they want us to carry 
	List Page
		Quick View Modal
			Add to cart
			tiny description
			price
			Wishlist Button
	Details Page 
		Add to cart
		Wishlist Button

**Cards To Sell**
	**List Page**
		Quick View Modal
			Add to cart
			tiny description
			price
			Wishlist Button
	**Details Page** 
		Add to cart
		Wishlist Button

Other Junk To Sell (Accessories. RenFair People? Misc Stuff) 
	List Page
		Quick View Modal
			Add to cart
			tiny description
			price
			Wishlist Button
	Details Page
		Add to cart
		Wishlist Button

**User Page**
	View In Store Credit
	View Table Reservations
	Opt in or out of alert for Calendar changes (of different types?)
	Order History 
		Optional match email at POS time to account db to add to this 
	**Wishlist Section**
		Ordered by sale things on top
		Ability to remove items from list
		Ability to opt in or out of email notifications when item on sale
		Navigate to Details Page
		Add to cart
		link to share wishlist with other people

Table Reservation page
	Possible Different types of tables (regular, DM tables, ...?)
	Show availability of table size and quantity 
	Reserve Button
		Date, Time, Party (estimate?),

**Cart Page (User Item Reservations)**
	See Items Reserved
	Price...
	See Date They marked as reserved
	Optional enter date/time you plan to pick up bundle 
		If entered, EOD after this, auto expires if not picked up
	See date their reservation expires (2days?)
	

**Bottom of site links**
	About Us
	Discord
	Instagram
	Facebook
	Contact us
	Exchange and Return Policy
# In-Store Worker Site
Employee Clock in/out Page?
View Work Schedule
	View who is working when (and their roles?)
	This should auto email to all employees once a week or something (when created?)

View Reserved Tables Calendar

View Event Calendar (Or they just look at Home Page?)

**View Orders Page**
	See: Email address, Name, Estimated Arrival Time, Items in bundle, 
	If employee should bundle prep or not (result of grief detection)
	Customer Bought Bundle (remove bundle from db)
	Customer Called in and said they dont want anymore -> expire reservation
		Bundles do not push to order history directly, only what they actually bought 
			e.g. they want 123, they show up, only want 12, buy 12, 123 does not push to order history for customer. only 12. 

# Admin Site
Employee Account Creation Page
	name, email, phone number
	
Manage Employee Availability Page
	See list of employee
	indicate when they are available or not
	
Manage Employees work schedule
	Who is working when (and what role? cashier/stocker/cafe?)
	Auto Email to all employee

Manage Events on Event Calendar
	Can add events when sets are coming out
	days things go on sale
	closed/open unexpected schedule
	other stuff
	Emails users 

Permission Management page
	Set who is admin
	Set who is In-Store Account  

**Manage Inventory Page**
	P.S. - All Individual Edits will be done on their Details page with admin creds 
	Set Sale % and Flat on BULK items based on a filtering system based on fields (Tags?) of products 
	**Add new products**

# Card Importer
Console app that pulls data from a tcgplayer csv file and then goes to scryfall to get more info then calls post endpoint to update backend server
- card name
- set name
- set number
- card type
- color
- rarity
- artist
- price

# Grief Protection
All Sell Reserve things 
One user cannot reserve > 20 things 
Check User Account Creation Date compared to Order limit 
Dont prepare bundles for young accounts with a lot of stock
Sus account greif detection
IP check? Sus anything outside 50mi radius? 
Some algorthim to determine if we prep bundle based on... acc create date, quantity of item, ip distance, repeat customer, ...?



# Sql Tables
**Index**
	Everything gets :
	created/createdby and modified/modifiedby
	Id
	Name
	Description
	ThumbnailImage
	DisplayImage
	List of images
	SellPrice
	WholesalePrice (Purchase price)
	Sale%
	SaleFlat
	SalePrice
	Stock
	
**Cards**
	everything that we get from tcg/scryfall
**Games**
	PlayerMinCount
	PlayerMaxCount
	GameTypes (list of GameType)
	AgeRangeMin
	AverageGameLength
	GameThemes (List of GameTheme)
	
**Users** 
	Id
	ThirdPartyId (Guid of user acc mgmt software)
	IsActive
	FirstName
	LastName
	Date of birth
	Email
	
**Wishlist**
	Id
	UserId FK
	ProductType (value from ProductTypes) FK
	ProductId FK

**Order**
	Id
	UserId FK
	OrderStatusId FK (value from OrderStatuses) 

**Cart**  
	 Id
	UserId FK
	OrderId FK
	ProductType (value from ProductTypes) FK
	ProductId FK

**Image**
	Id
	FK
	Type
	Binary
	FKTableName

**UserPermissions**
	Id
	UserId
	PermissionId
### List Tables
**GameTypes / Enum?**
	Id
	Name
**GameTheme / Enum?**
	Id
	Name
**OrderStatus / Enum?**
	Id
	Name
	Values: Purchased, ReadyForPickup, PickedUp
**ProductType / Enum?**
	Id
	Name
### Static Enum-esque object 
**Permissions**
	Id (Guid/int/string)
	Name
	Description
# Controllers
- Master
	- Get Many (Home page / mgmt bulk search page cross-object search)
- Card
	- Post Many (Importer)
	- Post Single (Mgmt page creation)
	- Patch Single (Stock, sale, etc changes)
	- Patch Many (bulk sale changes)
	- Get Many 
	- Get Single
- Game
	- Post Many (Importer)
	- Post Single (Mgmt page creation)
	- Patch Single (Stock, sale, etc changes)
	- Patch Many (bulk sale changes)
	- Get Many 
	- Get Single
- User
	- Post Single (Admin Create / User Acc Info from third party manager)
	- Possible splitting ^ to 2 endpoints
	- Patch Single (Change perms, first/last/dob/email/etc)
- Wishlist
	- Post Single (if none for user, auto create, this takes one new product id/type at a time)
	- Delete Single (Remove Single Item From Wishlist)
- Order
	- Patch Status 
- Cart
	- Post Single Item 
	- Delete Single Item
	- Purchase (Auto Creates Order once paid for)
- Image
	- Get
	- Post
	- Delete
- List (Controller for all list tables)
	- Post Single
	- Patch Single 
	- Delete Single


# Others
- Validations 
- 
