# BankManagement

---

![Imágen de las Entidades](./img/BankFixed.jpg)

## Diagrama Entidad Relación
![ER Diagram](./img/ERBankFixed.png)

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

---

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
  "requestDate": "2024-11-25T02:25:29.7848118Z",
  "status": "Pending"
}
```

## `4. Aprobación/Rechazo de Solicitudes`

`POST` `/api/Bank/Approve-Loan`
```json
{

}
```

`Response`
```json
{

}
```

Implementar un flujo de aprobación/rechazo para las solicitudes de préstamo, accesible únicamente por usuarios con un rol autorizado. El sistema debe permitir:
- **Rechazo**: Cambiar el estado de la solicitud a "Rechazada".
- **Aprobación**:
  - Cambiar el estado de la solicitud.
  - Guardar los datos relevantes en una entidad de "Préstamos Aprobados", incluyendo:
    - Cliente.
    - Fecha de aprobación.
    - Monto solicitado.
    - Plazo.
    - Tipo de préstamo.
    - Tasa de interés.
  - Generar automáticamente las cuotas correspondientes, guardando:
    - Monto total de la cuota.
    - Monto del capital correspondiente.
    - Monto del interés correspondiente.
    - Fecha de vencimiento (el día 1 de cada mes, comenzando desde el mes siguiente de su aprobación).

#### **Requisitos Técnicos:**
 - Solicitudes y Préstamos se guardan en tablas diferentes.
 - Si la solicitud es "Rechazada", validar que se establezca un motivo de forma obligatoria.

---

# Entidades de la Base de Datos