Table clients {
  id int pk
  name string
}

Table cars { 
  id int pk
  plate long
}

Table auto_parts {
  id int pk
  name string
  stock int 
  budgeted int
}

Table budgeted_parts {
  id int pk 
  auto_part_id int 
  budget_id int 
  quantity int
}

Table budget {
  id int pk
  client_id int 
  car_id int
}

Ref : budget.client_id > clients.id
Ref : budget.car_id > cars.id

Ref : budgeted_parts.budget_id > budget.id
Ref : budgeted_parts.auto_part_id > auto_parts.id
