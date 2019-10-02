import { Slucaj } from './../../model/Slucaj';
import { Slika } from './../../model/Slika';
import { Component, OnInit } from '@angular/core';
import { KorisnikService } from '../../service/korisnik.service';
import { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } from 'constants';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-kreiranje-slucaja',
  templateUrl: './kreiranje-slucaja.component.html',
  styleUrls: ['./kreiranje-slucaja.component.css']
})
export class KreiranjeSlucajaComponent implements OnInit {

  // slucaj = { opis: '' };
  slucaj = new Slucaj();
  fileData: File = null;
  // slika: Slika <Slika>;
  slike: Slika[] = [];
  base64textString = [];
  sirina;
  duzina;
  kategorije;
  podKategorije;
  originalData;
  i = 0;
  reader = new FileReader();
  private fileHandler: FileHandler;
  constructor(private korisnikService: KorisnikService) { }
  ngOnInit(): void {
    this.korisnikService.getAllKategorije().subscribe((res: any) => {
      this.originalData = res;
      this.kategorije = [...res].filter(x => !x.parentId);
    });
    this.reader.onload = this.handleReaderLoaded.bind(this);
  }
  onParentChanged(evt) {
    this.setSubcategories(evt.value);
  }

  setSubcategories(parentId) {
    this.podKategorije = [...this.originalData].filter(x => x.parentId === parentId);
  }
  kreiranjeSlucaja() {
    navigator.geolocation.getCurrentPosition((position) => {
      this.setGPS(position.coords.latitude, position.coords.longitude);
    });
    console.log(this.slucaj);
    this.slucaj.Slike = this.slike;
    this.korisnikService.kreiranjeSlucaja(this.slucaj);
  }
  setGPS(duzina, sirina) {
    this.slucaj.GDuzina = duzina;
    this.slucaj.GSirina = sirina;
  }
  onUploadChange(evt: any) {
    // let i = 0;

    const file = evt.target.files[0];
    if (file) {
      this.fileHandler = new FileHandler(file);
      this.reader.readAsBinaryString(file);
    }

  }

  handleReaderLoaded(e) {
    const base64 = btoa(e.target.result);
    console.log('data:image/png;base64,' + base64);
    console.log(e.target.result);
    // this.handlePictureName(file, slika);
    const slika = this.fileHandler.ProcessFile(base64);
    this.slike.push(slika);
    // this.slika[this.i].base64textString = btoa(e.target.result);
  }
}

class FileHandler {
  private slika: Slika;
  constructor(private file: File) {
    this.slika = new Slika();
  }

  public ProcessFile(base64): Slika {
    this.handlePictureName();
    this.slika.slikaProp = base64;
    return this.slika;
  }
  private handlePictureName() {
    this.slika.Naziv = this.file.name;
  }
}
