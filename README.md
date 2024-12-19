# OnlineShopDemo_VueJs
A single-page demo website built using Vue.js and Tailwind CSS for the frontend, with .Net core for the backend.

static website link (frontend-only with mock data) :
https://onlineshopdemo-shane.netlify.app/



<img src="https://github.com/user-attachments/assets/05d79218-2094-42b9-ac28-f42d321c0129" width = 900>

<p> </p>
<p> </p>

<b>Tools used</b>

	Node.js v22.12.0 , npm 10.9.0, Vue.js 3.5.13 , .NET SDK  8.0.404  , tailwindcss 3.4.17


<h2> Setting up instructions for testing the demo</h2>

<b>Setting Up the Frontend</b>


clone and navigate to the frontend directory:

	git clone https://github.com/OnlineShopDemo_VueJs.git
	cd OnlineShopDemo_VueJs/frontend

Install dependencies:

	npm install


Configure the frontend:

&emsp; Create a .env file in the frontend directory by copying .env.example.

&emsp; Set the VUE_APP_API_URL to point to the backend server.

&emsp; Set the VUE_APP_API_MODE = mock if frontend-only or VUE_APP_API_MODE = production if backend-connected

	
	#example
	VUE_APP_API_MODE=production
 	VUE_APP_API_URL=http://localhost:5065/api


Build the frontend for production:

	npm run build

Serve the frontend:

&emsp; You can use a static file server like npx serve:

	npx serve dist
&emsp; Or host the dist/ folder on your web server


<br></br>

<b>Setting Up the Backend</b>


navigate to backend:

	cd ../Backend

Restore dependencies:

	dotnet restore
 
Configure the backend:

&emsp; Create a .env file in the backend directory by copying .env.example.

&emsp; Update it to your frontend url

	#example
 	FRONTEND_URL=http://localhost:8080

Run the backend:

	dotnet run

<br></br>
<b>Notes</b>

For local development, you can run the frontend and backend simultaneously:

	Backend: dotnet run
 	Frontend: npm run serve
