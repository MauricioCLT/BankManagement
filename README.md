# BankManagement


## Ejecución del proyecto
### 1. Clonar el repositorio
```sh
git clone https://github.com/MauricioCLT/BankManagement.git && cd BankManagement
```

### 2. Crear un nuevo contenedor de PostgreSQL en Docker
`CMD`
```sh
docker container run ^
-dp 5432:5432 ^
--name postgres-bootcamp-prueba ^
-e POSTGRES_PASSWORD=123456 ^
-v postgres-bootcamp-prueba:/var/lib/postgresql/data ^
postgres:15.1
```

`POWERSHELL`
```ps1
docker container run \
-dp 5432:5432 \
--name postgres-bootcamp-prueba \
-e POSTGRES_PASSWORD=123456 \
-v postgres-bootcamp-prueba:/var/lib/postgresql/data \
postgres:15.1
```

### 3. Verificar en el connection string que todo este correcto.
```json
"ConnectionStrings": {
  "BankManagement": "Host=localhost; Database=bank; Username=postgres; Password=123456"
},
```

### 4. Aplicar las migraciones a la base de datos
```ps1
dotnet ef database update -p Infrastructure -s BankManagement
```

### 5. Datos para la prueba
`Customers`
```sql
INSERT INTO public."Customers"
("FirstName", "LastName", "Cid", "Email", "Phone", "Address", "Role")
VALUES('Clt', 'Api', 1234567, 'clt@gmail.com', '123456789', 'Ayolas 437, Asunción 001012', 'Customer');
```

`TermInterestRates`
```sql
INSERT INTO public."TermInterestRates"
        ("Months", "Interest")
values    (6,         9.81),
	      (12,        12.15),
	      (18,        15.15),
	      (24,        17.15),
	      (30,        19.15),
	      (36,        20.15),
	      (42,        21.15),
	      (48,        21.15);
```

### 5. Ejecución del proyecto
#### 5.1 Modo Debug
```ps1
cd BankManagement && dotnet run
```
#### 5.2 Modo Release
```ps1
cd BankManagement && dotnet -c Release && dotnet run
```

---

# Requerimientos del Sistema
## 1. `Creación de la Entidad "Plazo y Tasa de Interés`


## 2. `Simulador de Cuota`

`POST` `/api/SimulateLoan/Simulate-Credit`

```json
{
  "amount": 100000,
  "months": 6
}
```

`Simulate Credit Response`
```json
{
  "interestRate": 9.15,
  "monthyPayment": 17114.309416309,
  "totalPayment": 102685.856497854
}
```

## 3. `Solicitud de Préstamo`

`POST` `/api/Bank/Request-Loan`
```json
{
  "customerId": 1,
  "loanType": "Hipotecario",
  "months": 6,
  "amountRequest": 1000000
}
```

`Request Loan Response`

```json
{
  "customerId": 1,
  "loanRequestId": 12,
  "loanType": "Hipotecario",
  "months": 6,
  "amount": 1000000,
  "requestDate": "2024-11-25",
  "status": "Pending"
}
```

## `4. Aprobación/Rechazo de Solicitudes`

`POST` `/api/Bank/Approve-Loan`
```json
{
  "loanRequestId": 16,
  "customerId": 1
}
```

`Response`
```json
{
  "customerId": 1,
  "approvalDate": "2024-11-26T00:00:00",
  "requestedAmount": 78000,
  "months": 6,
  "loanType": "Personal",
  "interestRate": 9.15
}
```

`POST` `/api/Bank/Reject-Loan`
```json
{
  "loanRequestId": 18,
  "customerId": 1,
  "rejectedReason": "No cumple con los requisitos para el préstamo."
}
```

`Response`
```json
{
  "customerId": 1,
  "rejectReason": "No cumple con los requisitos para el préstamo."
}
```

`Si el prestamo ya fue aprobado o rechazado`
```json
{
  "Message": "La solicitud con ese {Id} ya fue procesada"
}
```


## `5. Consulta de Detalles de un Préstamo`

`GET` `/api/Bank/{loanRequestId}/Details`

`Response`
```json
{
  "customerId": 1,
  "customerName": "Mauricio Ramírez",
  "approvedDate": "2024-11-27T00:15:09.855923Z",
  "requestedAmount": 450000,
  "totalAmount": 545175,
  "revenue": 95175,
  "months": 6,
  "loanType": "Automotriz",
  "interestRate": 21.15,
  "completePayments": 5,
  "uncompletePayments": 1,
  "nextDueDate": "2025-05-01",
  "paymentStatus": "Pending payments"
}
```

## `6. Pago de Cuotas`
`POST` `api/Bank/{loanRequestId}/Pay-Installment`
```json
{
  "loanRequestId": 43,
  "installmentIds": [
    1, 2, 3, 4, 5
  ]
}
```

`Response`
```json
{
  "loanRequestId": 43,
  "paidInstallments": 5,
  "remainingInstallments": 1,
  "statusMessage": "Quedan algunas cuotas pendientes."
}
```

## `7. Listado de Cuotas`
`GET` `/FilterLoans/{loanId}/installments`

`Filter Default All` `FilterLoans/{25}/installments`

`Response`

```json
[
  {
    "id": 98,
    "totalAmount": 45754.166666666664,
    "dueDate": "2024-12-01T00:00:00Z",
    "status": "Complete"
  },
  {
    "id": 99,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-01-01T00:00:00Z",
    "status": "Complete"
  },
  {
    "id": 100,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-02-01T00:00:00Z",
    "status": "Pending"
  },
  {
    "id": 101,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-03-01T00:00:00Z",
    "status": "Pending"
  },
  {
    "id": 102,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-04-01T00:00:00Z",
    "status": "Pending"
  },
  {
    "id": 103,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-05-01T00:00:00Z",
    "status": "Pending"
  }
]
```

`Filter` `FilterLoans/{25}/installments?filter=Complete`

`Response`
```json
[
  {
    "id": 98,
    "totalAmount": 45754.166666666664,
    "dueDate": "2024-12-01T00:00:00Z",
    "status": "Complete"
  },
  {
    "id": 99,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-01-01T00:00:00Z",
    "status": "Complete"
  }
]
```

`Filter` `FilterLoans/{25}/installments?filter=Pending`

`Response`
```json
[
  {
    "id": 100,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-02-01T00:00:00Z",
    "status": "Pending"
  },
  {
    "id": 101,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-03-01T00:00:00Z",
    "status": "Pending"
  },
  {
    "id": 102,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-04-01T00:00:00Z",
    "status": "Pending"
  },
  {
    "id": 103,
    "totalAmount": 45754.166666666664,
    "dueDate": "2025-05-01T00:00:00Z",
    "status": "Pending"
  }
]
```

## `8. Listado de Cuotas Atrasadas`
Listar todas las cuotas atrasadas, mostrando:
- Cliente asociado.
- Fecha de vencimiento de la cuota.
- Días de atraso.
- Monto pendiente.

`Response`
```json
{

}
```

---

# Entidades de la Base de Datos

![Imágen de las Entidades](./img/BankFixed.jpg)

## Diagrama Entidad Relación
![ER Diagram](./img/ERBankFixed.png)