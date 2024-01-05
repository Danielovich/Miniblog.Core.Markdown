Use Markdown files from a public github repository as blogposts.

To do this we must first be able to configure where the markdown resources reside.

1. find a reasonable approach towards not downloading the resources too often.
	- When the app starts
	- On a recurring schedule, such as every 30 minutes

2. find a solution where a markdown file is used a blogpost within the original system.
	
3. find a solution for exposing the post through the IBlogService interface. GetPosts is relying the posts being in a collection, 
   which means we must have loaded the posts beforehand.


- Experiment. Expose a collection of available and transformed markdown files by using the initial collection.

