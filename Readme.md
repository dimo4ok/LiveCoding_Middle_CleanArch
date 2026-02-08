# Live Coding Task: Mid .NET + Azure Backend Developer

## Task Description

### Scenario
Build a **Product Catalog API** using .NET that fetches products from an external API, stores them in Azure Blob Storage, and provides CRUD operations.

### Requirements

#### 1. Fetch Products from External API and Save to Blob
Create a service that fetches products and automatically saves them to blob storage:

**Endpoint:**
- `POST /api/products/fetch` - Triggers fetch and save operation

**Service Implementation:**
The service should:
1. Use HttpClient to call https://dummyjson.com/products
2. Parse the JSON response
3. **Automatically save** all products to Azure Blob Storage (e.g., `products.json`)
4. Return success message with product count

#### 2. CRUD Operations with Azure Blob Storage (55 minutes)
Implement Azure Blob Storage service with CRUD operations:

**Required Endpoints:**
- `GET /api/products` - Get all products from blob storage
- `GET /api/products/{id}` - Get a specific product by ID from blob
- `PUT /api/products/{id}` - Update a product in blob storage
- `DELETE /api/products/{id}` - Delete a product from blob storage

**Implementation Notes:**
- Store products as JSON array in a single blob file (e.g., `products.json`)
- Read, modify, and write back the entire file for updates/deletes
- Use proper deserialization/serialization
- **Separation of concerns:** ProductService handles API fetch, BlobStorageService handles blob operations

#### 3. Configuration
Setup appsettings.json with Azure configuration