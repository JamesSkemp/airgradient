# AirGradient Basic Web API
Requires .NET 6.

## Building
`dotnet publish -c Release`

## Setup
The following is for my personal configuration, running on a local server, using nginx and systemd.

First, configure nginx for our dotnet application.
1. `sudo vim /etc/nginx/sites-enabled/default`
2. Add the following:

```
location /airgradient {
	proxy_pass http://127.0.0.1:5010;
	proxy_http_version 1.1;
	proxy_set_header Upgrade $http_upgrade;
	proxy_set_header Connection keep-alive;
	proxy_set_header Host $host;
	proxy_cache_bypass $http_upgrade;
	proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
	proxy_set_header X-Forwarded-Proto $scheme;
}
```

3. `sudo nginx -t`
4. `sudo systemctl restart nginx`

Next create a directory for the Web API.

1. `sudo mkdir /var/www/airgradient/html`
2. `sudo chown -R www-data:www-data /var/www/airgradient/html`

Copy files from local to remote:

1. `scp -r .\bin\Release\net6.0\publish\* user@<server>:/var/www/airgradient/html`

