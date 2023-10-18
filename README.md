_<details><summary>Original README</summary>_
Zadanie testowe

Wykonaj poniższe założenia gry typu First Person Shooter:
- Na scenie powinno znajdować się 5 obiektów do zniszczenia (np beczka, hydrant, etc.) oraz
gracz,
- Wymienione wyżej obiekty mają na sobie informacje z jakiego rodzaju materiału są
zrobione oraz ich wytrzymałość,
- Materiał nie jest powiązany z klasą material wbudowana w Unity, lecz określa z czego
fizycznie wykonany jest obiekt,
- Gracz powinien się poruszać jak w klasycznym FPS,
- Gracz posiada 3 różne bronie – każda broń ma określone jaki rodzaj obiektu niszczy oraz jak
duże obrażenia robi – sposób zmiany broni jest dowolny,
- Gdy gracz celuje w obiekt powinien móc go zniszczyć, jeśli broń którą aktualnie dzierży ma
możliwość uszkodzenia danego rodzaju materiału,
- Zniszczony obiekt powinien mieć możliwość podpięcia pod siebie dowolnych funkcji, które
wywoływać się będą po jego zniszczeniu – np wywołanie particle effects, lub otworzenie
drzwi, etc. Obiekt powinien móc przyjąć dowolne zachowanie bez konieczności dopisywania
kodu do jego klasy,
- Wszystkie elementy graficzne typu animacja, particle effects, etc. wpływaja korzystnie na
zadanie testowe,
- Wszystkie elementy zadania testowego powinny być traktowane jako część większego
rozszerzalnego systemu,
- Zadanie testowe powinno zostać wysłane jako pełny projekt wykonany w Unity3D w wersji
2021.3.5f1, prosimy również umieścić w projekcie informacje na temat sterowania.
  
Wykonane zadanie prosimy wysłać na adres e-mail: praca@beastgamesofficial.com

Beast Games S.A.
</details>

</br>

- [x] Unity 2021.3.5f1
- [x] klasyczny FPS
  - [x] testowa scena
    > zaimportowana z szablonu `First Person`, materiały podmienione na `Default-Material`
  - [x] kontrola   
    > `PlayerController` napisany od podstaw, wykorzystuje `CharacterController` i `InputSystem`
    > input jest zczytywany tylko i wyłącznie na eventach (zamiast pollingu co klatkę w `Update`)
    - [x] postać _(WSAD)_
      > poruszanie się z początkowym przyspieszeniem, grawitacja
    - [x] kamera _(myszka)_
      > lewo-prawo obraca cała postać, obrót góra-dól obraca tylko kamerami i bronią
    - [x] strzał _(lewy przycisk myszki)_
    - [x] zmiana broni _(prawy przycisk myszki)_
- [x] 3 bronie z atrubytami:
  > darmowe modele z asset store: [First Person Lover - Weapons Pack](https://assetstore.unity.com/packages/3d/props/guns/first-person-lover-weapons-pack-39976)
  - [x] zdawane obrażenia
    > bazowe obrażenia, modyfikowane przez poniższy system
  - [x] materiały, na które działa
    > system modyfikatorów obrażeń przeciwko poszczególnym materiałom, edytowalny w inspektorze. Przykład:  
    > jeśli trafisz w `drewno`, zadaj `200%` obrażeń  
    > jeśli trafisz w `plastik`, zadaj `100%` obrażeń  
    > jeśli trafisz w `metal`, zadaj `50%` obrażeń  
    > w przeciwnym wypadku zadaj `0%` obrażeń
- [ ] 5 zniszczalnych obiektów (np. beczka, hydrant) z atrybutami:
  - [ ] wytrzymałość
  - [ ] materiał (np. żelazo, plastik, drewno)
- [ ] event na zniszczenie obiektu
- [ ] rozszerzalne rozwiązania
- [ ] opcjonalnie: szata graficzna (animacje, particle)
