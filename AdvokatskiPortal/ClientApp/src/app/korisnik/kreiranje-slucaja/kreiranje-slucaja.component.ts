import { Slika } from './../../model/Slika';
import { Component } from '@angular/core';
import { KorisnikService } from '../../service/korisnik.service';
import { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } from 'constants';

@Component({
  selector: 'app-kreiranje-slucaja',
  templateUrl: './kreiranje-slucaja.component.html',
  styleUrls: ['./kreiranje-slucaja.component.css']
})
export class KreiranjeSlucajaComponent {
  slucaj = { opis: '' };
  fileData: File = null;
  // slika: Slika <Slika>;
  slika: Slika [] = [] ;
  base64textString = [];
  i = 0;
  constructor(private korisnikService: KorisnikService) { }

  kreiranjeSlucaja() {
    // this.korisnikService.kreiranjeSlucaja(this.slucaj);
    this.slika.forEach(e => {console.log(e)});
  }

  onUploadChange(evt: any) {
    // let i = 0;
    this.slika[this.i] = new Slika();
    console.log(this.slika[this.i]);
    const file = evt.target.files[0];
    this.handlePictureName(file, this.slika[this.i]);
    if (file) {
      const reader = new FileReader();
      this.handlePictureName(file, this.slika[this.i]);
      reader.onload = this.handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
      this.i++;
    }

  }
  handlePictureName(file, i) {
    this.slika[this.i].Naziv = file.name;

  }
  handleReaderLoaded(e, i) {
    this.slika[this.i].base64textString = 'data:image/png;base64,' + btoa(e.target.result);
    // i.base64textString = 'data:image/png;base64,' + btoa(e.target.result);
    // this.slika[i].base64textString.push('data:image/png;base64,' + btoa(e.target.result));
    console.log(this.slika[this.i].base64textString);

    // this.base64textString.push('data:image/png;base64,' + btoa(e.target.result));
  }


}
