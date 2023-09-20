# Getting started

## API usage examples

1. Run API project

2. Call with curl, for example:

```
curl --location --request GET 'https://localhost:44312/api/Trial/GetTrialWinner' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer {token}' \
--data '{
    "PlaintiffSignatures": "KNV",
    "DefendantSignatures": "KKKKK"
}'

curl --location --request GET 'https://localhost:44312/api/Trial/GetMinimumSignatureToWin' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer {token}' \
--data '{
    "PlaintiffSignatures": "K#NV",
    "DefendantSignatures": "KKKKK"
}'
```


### Token generation

Go to API project folder and run the generator command:

```
cd .\LobbyWarsBoundedContext\src\API\
dotnet user-jwts create --scope "lobby_wars_api" --role "admin" --output "token"
```