****Instructions to Run ****

cd ~\Point72_Assessment\Trade_Presentation

dotnet restore Point72_Assessment.sln

dotnet build Point72_Assessment.sln

dotnet run Point72_Assessment.sln



**Access Swagger UI at:**
http://localhost:5055/swagger/index.html


**API Endpoints**
**Trades**
POST /api/trades - Create a new trade
GET /api/trades - Get all trades
GET /api/trades/{id} - Get trade by ID
GET /api/trades/symbol/{symbol} - Get trades by symbol

**Positions**
GET /api/positions - Get all positions
GET /api/positions/{symbol} - Get position for a symbol

