import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-prikaz-slucaja-korisnik',
  templateUrl: './prikaz-slucaja-korisnik.component.html',
  styleUrls: ['./prikaz-slucaja-korisnik.component.css']
})
export class PrikazSLucajaKorisnikComponent {
  images: any[] = [];
  constructor( public dialogRef: MatDialogRef<PrikazSLucajaKorisnikComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }
    // console.log(data.slike);
    // this.images = data.slike.map((s) => s.slikaProp);
    // console.log(this.images);

    close(): void {
    this.dialogRef.close();
  }
 }
