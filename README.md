# ViestiApi Back-end harjoitustyö

## Sovelluksen rakenne
> [!Note]
> #### Viestintäsovelluksen backend. Projekti on rakennettu ASP.NET Core sovelluksena C# kielellä. Se on jaettu useisiin kansioihin joilla jokaisella on oma tehtävä.  
1. Controllers <sup>API-pyyntöjen käsittely</sup>  
   - ***MessageController.cs*** vastaanottaa viestipyynnön ja ohjaa sen sovelluksenkäsiteltäväksi
   - ***UsersController.cs*** vastaanottaa käyttäjäpyynnöt ja ohjaa sen sovelluksenkäsiteltäväksi
     
2. Middleware <sup>Pyyntöjen tarkistukset ja tunnistautuminen</sup>
   - ***ApiKeyMiddleware.cs*** Tarkistaa API-avaimen; Jos API-avain puuttuu tai on virheellinen, sovellus palauttaa virheen. Jos API-avain on oikea, pyyntö pääsee etenemään.
   - ***BasicAuthenticationHandler.cs*** Käsittelee käyttäjän tunnistautumisen käyttäjätunnuksella ja salasanalla
   - ***UserAuthenticationService.cs*** Tarkistaa käyttäjän salasanan ja käyttäjien oikeudet
4. Models <sup>Tietomallit ja tietokantayhteys</sup>
   - ***User.cs*** Määrittelee mitä tietoja käyttäjästä tallennetaan: Id, Username, Password, Lastname, Joindate, Lastlogin, Salt,
   - ***Message.cs*** Sovelluksessa lähetettävä viesti. Sisältää: Id, Title, Body, Sender, Receiver, PrevMessage, SendTime
     - ***MessageDTO*** Määrittelee millaisessa muodossa viestin tiedot siirretään sovelluksen ja käyttäjän välillä.
   - ***MessageContext.cs*** Määrittelee mitä tietokannan taulua käytetään
5. Repositories <sup>Tietojen hakeminen ja tallentaminen</sup>
   - ***MessageRepository.cs*** Tämä repository sisältää viestien käsittelyyn liittyvät toiminnot kuten viestien lisääminen, hakeminen, päivittäminen ja poistaminen
   - ***IMessageRepository.cs*** Määrittelee mitä viesteihin liittyviä toimintoja voidaan tehdä.
   - ***UserRepository.cs*** Tämä repository sisältää käyttäjien käsittelyyn liittyvät toiminnot kuten käyttäjien lisääminen, hakeminen, päivittäminen ja poistaminen
   - ***IUserRepository.cs*** Määrittelee mitä käyttäjiin liittyviä toimintoja voidaan tehdä.
8. Services <sup>Sovelluksen toimintalogiikka</sup>
   - ***MessageService.cs*** Huolehtii viestien käsittelystä ennen niiden tallentamista ja käyttäjälle palauttamista. Asettaa viestin lähetysajan, selvittää lähettäjän ja vastaanottajan käyttäjätiedot sekä muuntaa viestit DTO muotoon.
   - ***IMessageService.cs*** Määrittelee mitä viestien käsittelyyn liittyviä toimintoja muut sovelluksen osat voivat käyttää.
   - ***UserService.cs*** Huolehtii käyttäjien luomiseen ja käsittelyyn liittyvästä logiikasta. Tarkistaa onko käyttäjänimi jo käytössä, asettaa käyttäjän liittymispäivän ja huolehtii uuden käyttäjän salasanan käsittelystä.
   - ***IUserService.cs*** Määrittelee mitä käyttäjien käsittelyyn liittyviä toimintoja muut sovelluksen osat voivat käyttää.
9. Migrations <sup>Tietokannan rakenteen muutokset</sup>   
   - Migrations kansio siältää tiedostot joiden avulla tietokannan rakennetta voidaan luoda ja muuttaa sovelluksen kehityksen aikana.
     - ***IntialCreate*** Luo tietokannan alkuperäisen rakenteen. Se sisältää Users ja Messages taulut.
     - ***AddSalt*** Lisää Users tauluun uuden Salt sarakkeen jota käytetään salasanojen turvaamisessa. 
## Rajapinnat
ViestiApi tarjoaa käyttäjien ja viestien hallintaan tarkoitetun HTTP-rajapinnan, jonka avulla voi luoda käyttäjätilejä, kirjautua sisään, lähettää, hakea ja hallita viestejä.  
Rajapinta on jaettu kahteen osaan: *Users* ja *Messages*.
   - *Users* rajapinta mahdollistaa käyttäjien hallinnan.
   - *Messages* rajapinta mahdollistaa viestien käsittelyn.
      
Rajapinta käyttää seuraavia HTTP metodeja:
   - **GET** tietojen hakeminen
   - **POST** uusien tietojen luominen
   - **PUT** olemassa olevien tietojen päivittäminen
   - **DELETE** tietojen poistaminen


## Sovelluksen käyttö


## Viestin lähetys

