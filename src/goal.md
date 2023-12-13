Use Markdown files from an online resource as blogposts.

To do this we must first be able to configure where the markdown resources reside.

A challenge is to find a reasonable approach towards not downloading the resources too often.
	- When the app starts
	- On a recurring schedule, such as every 30 minutes

Next is to find a solution where a markdown file is used a blogpost within the original system.
	- The IBlogService interface GetPosts is relying the posts being in a collection, which means we must
	have loaded the posts beforehand.


- Expose an initial collection of possible markdown files available.
- Experiment. Expose a collection of available and transformed markdown files by using the initial collection.

