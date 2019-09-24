import { Component } from '@angular/core';
import { KorisnikService } from '../../service/korisnik.service';

@Component({
  selector: 'app-kreiranje-slucaja',
  templateUrl: './kreiranje-slucaja.component.html',
  styleUrls: ['./kreiranje-slucaja.component.css']
})
export class KreiranjeSlucajaComponent {
  slucaj = { opis: '' };
  fileData: File = null;
  constructor(private korisnikService: KorisnikService) { }

  kreiranjeSlucaja() {
    // this.korisnikService.kreiranjeSlucaja(this.slucaj);
    console.log(this.fileData)
  }

  fileProgress(fileInput) {
    this.fileData = <File>fileInput;
    const x = <File> fileInput.imageAsBase64;
    console.log(fileInput.target.files[0]);
  }

}
