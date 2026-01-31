Req:
{
  "saleNumber": "SALE-2026-001",
  "saleDate": "2026-01-31T15:00:00Z",
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "customerName": "João Silva",
  "branchId": "7ca85f64-5717-4562-b3fc-2c963f66afb1",
  "branchName": "Filial São Paulo",
  "items": [
    {
      "productId": "9fa85f64-5717-4562-b3fc-2c963f66afc2",
      "productTitle": "Cerveja Ambev 600ml",
      "quantity": 5,
      "unitPrice": 10.00
    }
  ]
}

10%
{
  "data": {
    "id": "792938f9-4eb0-4ed5-8f79-33754ef92644",
    "saleNumber": "SALE-2026-001",
    "totalAmount": 45
  },
  "success": true,
  "message": "Sale created successfully",
  "errors": []
}


Requisição
{
  "saleNumber": "SALE-TEST-001",
  "saleDate": "2026-01-31T18:00:00Z",
  "customerId": "11111111-1111-1111-1111-111111111111",
  "customerName": "Cliente Sem Desconto",
  "branchId": "22222222-2222-2222-2222-222222222222",
  "branchName": "Filial A",
  "items": [
    {
      "productId": "33333333-3333-3333-3333-333333333333",
      "productTitle": "Produto A",
      "quantity": 3,
      "unitPrice": 100.0
    }
  ]
}

sem desconto
{
  "data": {
    "id": "e7eccd55-a697-4cfc-9bfb-475069dd3629",
    "saleNumber": "SALE-TEST-001",
    "totalAmount": 300
  },
  "success": true,
  "message": "Sale created successfully",
  "errors": []
}




Requisição
{
  "saleNumber": "SALE-TEST-003",
  "saleDate": "2026-01-31T18:00:00Z",
  "customerId": "77777777-7777-7777-7777-777777777777",
  "customerName": "Cliente 20 Porcento",
  "branchId": "88888888-8888-8888-8888-888888888888",
  "branchName": "Filial C",
  "items": [
    {
      "productId": "99999999-9999-9999-9999-999999999999",
      "productTitle": "Produto C",
      "quantity": 15,
      "unitPrice": 100.0
    }
  ]
}

20% desconto

{
  "data": {
    "id": "072a8bc1-60c7-418b-bdf2-83a6ce5c763c",
    "saleNumber": "SALE-TEST-003",
    "totalAmount": 1200
  },
  "success": true,
  "message": "Sale created successfully",
  "errors": []
}



Requisição

{
  "saleNumber": "SALE-TEST-004",
  "saleDate": "2026-01-31T18:00:00Z",
  "customerId": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
  "customerName": "Cliente Excede Limite",
  "branchId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
  "branchName": "Filial D",
  "items": [
    {
      "productId": "cccccccc-cccc-cccc-cccc-cccccccccccc",
      "productTitle": "Produto D",
      "quantity": 25,
      "unitPrice": 100.0
    }
  ]
}

+20 itens
	
Error: Bad Request

Response body
Download
{
  "success": false,
  "message": "Validation Failed",
  "errors": [
    {
      "error": "LessThanOrEqualValidator",
      "detail": "Cannot sell more than 20 identical items"
    }
  ]
}


GET ID
792938f9-4eb0-4ed5-8f79-33754ef92644

{
  "data": {
    "data": {
      "id": "792938f9-4eb0-4ed5-8f79-33754ef92644",
      "saleNumber": "SALE-2026-001",
      "saleDate": "2026-01-31T15:00:00Z",
      "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "customerName": "João Silva",
      "branchId": "7ca85f64-5717-4562-b3fc-2c963f66afb1",
      "branchName": "Filial São Paulo",
      "status": "Active",
      "isCancelled": false,
      "totalAmount": 45,
      "items": [
        {
          "id": "d4a3e3f4-e707-4f70-ad38-cfb79dd34db9",
          "productId": "9fa85f64-5717-4562-b3fc-2c963f66afc2",
          "productTitle": "Cerveja Ambev 600ml",
          "quantity": 5,
          "unitPrice": 10,
          "discount": 10,
          "totalAmount": 45,
          "isCancelled": false
        }
      ],
      "createdAt": "2026-01-31T19:24:41.709924Z",
      "updatedAt": "2026-01-31T19:24:41.710692Z"
    },
    "success": true,
    "message": "Sale retrieved successfully",
    "errors": []
  },
  "success": true,
  "message": "",
  "errors": []
}


CANCEL ID
792938f9-4eb0-4ed5-8f79-33754ef92644

{
  "data": {
    "data": {
      "id": "792938f9-4eb0-4ed5-8f79-33754ef92644",
      "saleNumber": "SALE-2026-001",
      "isCancelled": true
    },
    "success": true,
    "message": "Sale cancelled successfully",
    "errors": []
  },
  "success": true,
  "message": "",
  "errors": []
}



PUT
Update

{
  "data": {
    "data": {
      "id": "e7eccd55-a697-4cfc-9bfb-475069dd3629",
      "saleNumber": "SALE-TEST-001",
      "totalAmount": 720
    },
    "success": true,
    "message": "Sale updated successfully",
    "errors": []
  },
  "success": true,
  "message": "",
  "errors": []
}


Getall

{
  "data": {
    "data": [
      {
        "id": "072a8bc1-60c7-418b-bdf2-83a6ce5c763c",
        "saleNumber": "SALE-TEST-003",
        "saleDate": "2026-01-31T18:00:00Z",
        "customerId": "77777777-7777-7777-7777-777777777777",
        "customerName": "Cliente 20 Porcento",
        "branchId": "88888888-8888-8888-8888-888888888888",
        "branchName": "Filial C",
        "status": "Active",
        "isCancelled": false,
        "totalAmount": 1200,
        "items": [
          {
            "id": "00d818d8-0acf-4115-ac5e-9667f3fe5e1c",
            "productId": "99999999-9999-9999-9999-999999999999",
            "productTitle": "Produto C",
            "quantity": 15,
            "unitPrice": 100,
            "discount": 20,
            "totalAmount": 1200,
            "isCancelled": false
          }
        ],
        "createdAt": "2026-01-31T19:59:34.724974Z",
        "updatedAt": "2026-01-31T19:59:34.724983Z"
      },
      {
        "id": "792938f9-4eb0-4ed5-8f79-33754ef92644",
        "saleNumber": "SALE-2026-001",
        "saleDate": "2026-01-31T15:00:00Z",
        "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "customerName": "João Silva",
        "branchId": "7ca85f64-5717-4562-b3fc-2c963f66afb1",
        "branchName": "Filial São Paulo",
        "status": "Cancelled",
        "isCancelled": true,
        "totalAmount": 45,
        "items": [
          {
            "id": "d4a3e3f4-e707-4f70-ad38-cfb79dd34db9",
            "productId": "9fa85f64-5717-4562-b3fc-2c963f66afc2",
            "productTitle": "Cerveja Ambev 600ml",
            "quantity": 5,
            "unitPrice": 10,
            "discount": 10,
            "totalAmount": 45,
            "isCancelled": false
          }
        ],
        "createdAt": "2026-01-31T19:24:41.709924Z",
        "updatedAt": "2026-01-31T20:02:45.468858Z"
      },
      {
        "id": "e7eccd55-a697-4cfc-9bfb-475069dd3629",
        "saleNumber": "SALE-TEST-001",
        "saleDate": "2026-01-31T18:00:00Z",
        "customerId": "44444444-4444-4444-4444-444444444444",
        "customerName": "Cliente 10 Porcento Editado",
        "branchId": "55555555-5555-5555-5555-555555555555",
        "branchName": "Filial B",
        "status": "Active",
        "isCancelled": false,
        "totalAmount": 720,
        "items": [
          {
            "id": "2b6223b5-2894-44fd-8382-77830ad638ef",
            "productId": "66666666-6666-6666-6666-666666666666",
            "productTitle": "Produto B",
            "quantity": 10,
            "unitPrice": 90,
            "discount": 20,
            "totalAmount": 720,
            "isCancelled": false
          }
        ],
        "createdAt": "2026-01-31T19:58:47.982259Z",
        "updatedAt": "2026-01-31T20:04:27.228553Z"
      }
    ],
    "success": true,
    "message": "Retrieved 3 sales successfully",
    "errors": []
  },
  "success": true,
  "message": "",
  "errors": []
}