import { element } from 'protractor';
import { AdvokatService } from './../../../service/advokat.service';
import { Cenovnik } from './../../../model/Cenovnik';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-prepraviti-ponudu',
  templateUrl: './prepraviti-ponudu.component.html',
  styleUrls: ['./prepraviti-ponudu.component.css']
})
export class PrepravitiPonuduComponent implements OnInit {
  public test: any;
  cenovnik: Cenovnik;
  private submitCallback: Function;
  hideUserOptions: boolean;

  constructor(private advokatService: AdvokatService,
    public dialogRef: MatDialogRef<PrepravitiPonuduComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.cenovnik = data.cenovnik;
    this.cenovnik.pocetakRada = data.pocetakRada;
    this.cenovnik.zavrsetakRada = data.zavrsetakRada;
    this.submitCallback = data.submitCallback;
    this.hideUserOptions = data.hideUserOptions;
  }

  ngOnInit() {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onYesClick(): void {
    if (this.submitCallback) {
      const cenovnik = {...this.cenovnik};
      cenovnik.slucaj.slike.forEach((slika: any) => {
        if (slika.slikaProp) {
         const base64result = slika.slikaProp.split(',')[1];
         slika.slikaProp = base64result;
        }
      });
      cenovnik.kolicina = this.data.kolicina;
      cenovnik.vrstaPlacanja = this.data.vrstaPlacanja;
      cenovnik.pocetakRada = this.data.pocetakRada;
      cenovnik.zavrsetakRada = this.data.zavrsetakRada;
      cenovnik.komentar = this.data.komentar;
      cenovnik.isKonacan = this.data.isKonacan;
      this.submitCallback(cenovnik);
    }
  }

}
