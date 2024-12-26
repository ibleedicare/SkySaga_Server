# API Call

api_call.csv are "/api" calls extracted using Ghidra

Each endpoints are created in the "Endpoints" folder.

# Trafic analysis

When launching the SkySaga Game client, the game makes several API calls.

For the authentication, here how it works : 

1. The first API call is made to `/api/authentication/applications/names/login` (see `openapi.yml` for details of the request).

2. Once logged in, the game sends a new request to `/api/authentication/sgauth/_login` with an `X-RWPVT` header. This header is the tokenId retrieve from the previous API call

3. Once logged with the previous API call, the game do a new API call to `/api/game-conductor/geonode`

Once the authentication is done, the game do an API call on the matchmaking endpoint to join a server: `/api/matchmaking/userdatacentre/create`

After this API endpoint has been call the game do a request to `/api/account/get`


