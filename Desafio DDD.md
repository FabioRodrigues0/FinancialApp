---
title: Desafio DDD
created: "2022-04-11T08:11:46.053Z"
modified: "2022-04-14T14:00:33.087Z"
---

# Desafio DDD

## Models

Cada classe vai derivar da ClassModel base com já o Id nela.

### Pedido

<details>
  <summary></summary>
- ID GUID <>
- Code long
- Date DateTime
- DeliveryDate DateTime
- Products
  - ProductDescription string
  - ProductCategory int
  - Quantity decimal
  - Value decimal
- Client GUID
- ClientDescription string
- ClientEmail string
- ClientPhone string
- Status enum
- Street string
- Number string
- Sector string,
- Complement string
- City string
- State string
- Discount decimal
- Cost decimal
</details>

### Documentos

<details>
  <summary></summary>

- ID GUID <>
- Number string
- Date DateTime
- DocumentType int
- Operation int
- Paid bool
- PaymentDate DateTime
- Description string
- Total decimal
- Observation string
</details>

### Livro Caixa

<details>
  <summary></summary>

- ID <>
- Origem
- OrigemID
- Descrição
- Tipo
- Valor
</details>

## Notas

- Procurar ou perguntar se e susposto o container da base de dados se resetar quando se faz rebuild ou clean build

## Importante

