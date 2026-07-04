# Features

- Base features (Basically copy of Backloggd)
	- Game page
		- [X] View
			- Name
			- Alternative Names
			- Version_Title
			- Cover
			- Summary
			- Platforms
			- Supported languages
			- Genres
			- Developers
			- Screenshots
			- Release date
		- [ ] Reviews
			- Comments
			- Rating
		- [ ] Likes
	- Main page
		- [ ] Search
		- [ ] Follow feed
	- Profile
		- [ ] pfp picture
		- [ ] Follows
		- [ ] Game lists
		- [ ] Notifications
			- New like
			- New comment
	- Technical stuff
		- [ ] Migrate to CQRS with MediatR
		- [ ] Deal with uuid's in urls
        - [ ] Middleware or something else to retrieve user identity from request
	- Features
		- [ ] Comment with some amount of upvotes shows automatically under review

Additional features
- Recommend unpopular/old games (called "blow")
  tracking of pages which last visit was more than 30 days and display it in separate section
- feed of upcoming releases (possible realtime Pog)
- Recently trending