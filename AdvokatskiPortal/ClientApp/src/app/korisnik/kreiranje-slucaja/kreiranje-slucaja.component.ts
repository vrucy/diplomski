import { Slucaj } from './../../model/Slucaj';
import { Slika } from './../../model/Slika';
import { Component, OnInit } from '@angular/core';
import { KorisnikService } from '../../service/korisnik.service';
import { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } from 'constants';
import { DatePipe } from '@angular/common';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';

@Component({
  selector: 'app-kreiranje-slucaja',
  templateUrl: './kreiranje-slucaja.component.html',
  styleUrls: ['./kreiranje-slucaja.component.css']
})
export class KreiranjeSlucajaComponent implements OnInit {
  slucaj = new Slucaj();
  fileData: File = null;
  slike: Slika[] = [];
  base64textString = [];
  sirina;
  duzina;
  kategorije;
  podKategorije;
  originalData;
  i = 0;
  pdfSrc = "";
  reader = new FileReader();
  private fileHandler: ImageHandler;
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
      this.slucaj.slike = this.slike;
      this.korisnikService.kreiranjeSlucaja(this.slucaj);
    });
  }
  setGPS(duzina, sirina) {
    this.slucaj.GDuzina = duzina;
    this.slucaj.GSirina = sirina;
  }
  onUploadChange(evt: any) {
    // let i = 0;

    const file = evt.target.files[0];
    if (file) {
      this.fileHandler = new ImageHandler(file);
      this.reader.readAsDataURL(file);
    }

  }
  prikazSlike;
  handleReaderLoaded(e) {
    // console.log(e.target.result)
    const base64 = e.target.result.toString().split(',')[1];
    const prikaz = e.target.result;
    this.pdfSrc = e.target.result;
    console.log(e.target.result.toString().split(';')[0])
    let type ;
    if(e.target.result.toString().split(';')[0] === "data:application/pdf") {
      type = "pdf";
      console.log('pdf fajl je uploudovan ')
    }

    const slika = this.fileHandler.ProcessFile(base64, prikaz, type);
    this.slike.push(slika);
    this.prikazSlike = this.slike;
    console.log(this.prikazSlike)
    // this.slika[this.i].base64textString = btoa(e.target.result);
  }
  deleteImage(img):string {
    this.slike.forEach(slika => {
      if (slika === img ){
        const index: number = this.slike.indexOf(img);
        this.slike.splice(index ,1)
        console.log('slika: ', slika)
      }
    });
    return img;
  }
}

class ImageHandler {
  private slika: Slika;
  constructor(private file: File) {
    this.slika = new Slika();
  }

  public ProcessFile(base64, prikaz, type): Slika {
    this.handlePictureName();
    this.slika.type = type;
    this.slika.slikaProp = base64;
    this.slika.prikaz = prikaz;
    console.log(this.slika);
    return this.slika;
  }
  private handlePictureName() {
    this.slika.Naziv = this.file.name;
  }
}
