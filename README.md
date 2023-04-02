# GPS Golf
<img width="1000" alt="golfkuvat" src="https://user-images.githubusercontent.com/117892331/225332419-b2fededc-2179-4ebe-8ac7-fdddd4af071b.png">

*C#, Unity, Xml, Google Cloud, Visual Studio*

**GPS GOLF on AR peli kännykälle, joka käyttää GPS paikannusta pääpelimekaniikkanaan. Tarkoitus on golfata pallot lipputankoihin jotka päivittyvät satunnaisesti joka päivä. Kartalle on merkitty tähän mennessä kaikki Suomen tiet, jotka on tallennettu Xml tiedostoille Google Cloudiin. Tiedot on kerätty OpenStreetMap sivustolta. Pelimoottorina on Unity.**

- Tein pelille erillisen OSM xml datan muokkaus ohjelman Visual Studiota käyttäen, koska en löytänyt netistä vastinetta. Ohjelma käytti trie data struktuuria yhdistämään ja suodattamaan n. 900 miljoonaa Suomen teiden koordinaatteja sopivampaan muotoon (OSM datan tie merkinnöissä oli vain viittaukset varsinasiin koordinaatteihin). Sekä lajitteli n.5 neliökilometrin kokoisia alueita koordinaattien perusteella nimettyihin kansioihin. Jotka sitten lopulta latasin Google Cloudiin. Tein tästä 5 eri versiota joista ensimmäisellä olisi menny 5 vuotta lajitella kaikki koordinaatit. Sain ajan vähennettyä viimeisimmässä versiossa 5 minuuttiin.
- Tiet on piirretty 3D pallon muotoiselle objektille, koska 2D alustalla koordinaatit vääristyivät hieman. 3D koordinaatit on laskettu mapallon säteen mukaan.
- Tiet on piirretty Unityn kolmioiden piirto työkaluilla tehden mahdollisimman yhtenäisiä objekteja, jotta suorituskyky ei kärsisi. Minulle tuli aika paljon ongelmia saada tiet oikein päin ja ylipäätään näkyviin.
- Peli objektien koot muokkautuvat kameran etäisyyden perusteella
- Tämä oli yksi ensimmäisistä projekteistani joten siinä meni noin 2 kuukautta. Opin yllättävän paljon data struktuureista ja järjestely algoritmeista sekä myös Google Cloudin käyttöä
