# Customer Site Pages

**Home Page** 
	**Search bar at top**

**Board Game To Sell**
	**List Page**
	**Details Page** 
		Add to cart
		Wishlist Button

**Cards To Sell**
	**List Page**
	**Details Page** 
		Add to cart
		Wishlist Button

User Page
	Wishlist Section
		Ordered by sale things on top
		Ability to remove items from list
		Ability to opt in or out of email notifications when item on sale
		Navigate to Details Page
		Add to cart
		link to share wishlist with other people

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

**View Orders Page**
	See: Email address, Name, Estimated Arrival Time, Items in bundle, 
	If employee should bundle prep or not (result of grief detection)
	Customer Bought Bundle (remove bundle from db)
	Customer Called in and said they dont want anymore -> expire reservation
		Bundles do not push to order history directly, only what they actually bought 
			e.g. they want 123, they show up, only want 12, buy 12, 123 does not push to order history for customer. only 12. 

# Admin Site
**Manage Inventory Page**
	**Add new products**

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