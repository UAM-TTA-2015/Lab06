# Zadanie domowe

Uwaga: Każde z zadań implementujemy zgodnie z zasadami TDD - wpierw tworzymy testy a potem stopniowo implementacje.

## 1. Dodać kolejne obiekty do naszego kontekstu bazodanowego (```UamTTAContext```)
* BudgetTemplate
* Budget
* Transaction

## 2. Dodać migracje tworzace kolejne tabele w bazie danych

## 3. Zaimplementować metode IRepository<T>.Take w klasie ```EFRepository```
```IEnumerable<T> Take(int count);```
Metoda zwraca pierwsze ```count``` elementów z naszego repozytorium. Jezeli repozytorium jest puste lub zawiera mniej niż ```count``` elementów to wyrzuca wyjatek ```ArgumentException```.

## 4. Zaimplementować metode IRepository<T>.GetByIds w klasie ```EFRepository```
```IEnumerable<T> GetByIds(IEnumerable<int> ids);```
Metoda zwraca wszystkie elementy o id podanych w parametrze ```ids```. Jeżeli element o podanym id nie istnieje to go pomijamy.
