# Aplikacja do zarządzania domowym budżetem

## Ogólny opis aplikacji
Aplikacja ma służyć do zarządzania domowymi finansami. Powinna posiadać możliwość definiowania budżetów na wybrany okres czasu (np. miesiąc) oraz sprawdzenia jego realizacji.
Aby śledzić poszczególne wpływy i wydatki, każde z nich będzie powiązane z jednym ze zdefiniowanych w systemie kont.
Konta te mogą być powiązane z kontem bankowym, ale nie muszą - mogą być wirtualnym rozliczeniem wpływów i wydatków.
Poszczególne operacje będą powiązane z kontem źródłowym i docelowym, a także budżetem w ramach którego będą rozliczone.

## Podstawowe encje w systemie
* **Konto** - grupuje transakcje danego typu.
  * Konto może być wewnętrzne lub zewnętrzne 
  * Konta wewnętrzne muszą się bilansować, tzn. operacja nie może wziąć środków "z próżni"
  * Konta zewnętrzne z reguły będą niezbilansowane, np. konto Pensja będzie dostarczać środki do systemu, a konto Czynsz będzie je konsumować
  * Konto może mieć przypisane konto rozliczeniowe / zamykające, na które będą przekazane środki po zakończeniu budżetu
  * Konto może mieć wymagać stanu nieujemnego lub mieć limit kredytowy (zdefiniowany lub nieskończony - do zaimplementowania jako stan minimalny dla konta)
  * Konto może mieć stan docelowy, np. konto oszczędnościowe   
* **Transakcje** - definiują operację przepływu środków pomiędzy kontami.
  * Transakcje zawsze mają dwie strony - konto źródłowe i docelowe
  * Mogą być jednorazowe lub okresowe (powtarzające się)
  * Transakcje mogą mieć ustalony termin wykonania z opcją potwierdzenia wymagania lub automatycznym wykonaniem o zadanym czasie (status transakcji - zaplanowana lub zrealizowana)  
* **Szablon budżetu** - pozwala zdefiniować powtarzalny schemat budżetu, który będzie generowany co wybrany okres czasu.
  * Na podstawie szablonu budżetu będziemy generować kolejne budżety
* **Budżet** - grupuje operacje w ramach danego okresu czasu. Pozwala rozliczyć wpływy i wydatki oraz pokazać bilans środków na poszczególnych kontach.
  * Każdy budżet ma datę początkową oraz końcową
  * W ramach budżetu powinno się znaleźć podsumowanie transakcji wg poszczególnych kont 

## Podstawowe operacje
* Utworzenie listy kont
* Utworzenie szablonu budżetu
* Utworzenie budżetu na podstawie szablonu
* Otworzenie budżetu
* Dodawanie transakcji do budżetu
* Zamknięcie budżetu

## Założenia systemu
* Możemy mieć wiele aktywnych budżetów
  * Przykład: osobny budżet osobisty, rodzinny i firmowy
* Każdy budżet może mieć inny okres rozliczeniowy
  * Np. budżet firmowy może być rozliczany co 3 miesiące a osobisty co miesiąc
* Stan końcowy środków na koncie na koniec jednego budżetu powinien być przeniesiony jako stan początkowy w nowym budżecie
  * Środki mogą być przeniesione na to samo konto lub konto rozliczeniowe (jeżeli jest zdefiniowane dla danego konta)
