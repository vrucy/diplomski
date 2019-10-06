import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-prikaz-slucaja-korisnik',
  templateUrl: './prikaz-slucaja-korisnik.component.html',
  styleUrls: ['./prikaz-slucaja-korisnik.component.css']
})
export class PrikazSLucajaKorisnikComponent {

  constructor( public dialogRef: MatDialogRef<PrikazSLucajaKorisnikComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

    close(): void {
    this.dialogRef.close();
  }
 }
