import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { Advokat } from '../../model/Advokat';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-slanje-slucaja',
  templateUrl: './slanje-slucaja.component.html',
  styleUrls: ['./slanje-slucaja.component.css']
})
export class SlanjeSlucajaComponent implements OnInit {
  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thridFormGroup: FormGroup;
  Cenovniks = { vrstaPlacanja: '', kolicina: ''} 
  vrstaPlacanja
  displayedColumns: string[] = ['select', 'Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  selection = new SelectionModel<Advokat>(true, []);
  public dataSource = new MatTableDataSource<Advokat>();
  slucaj = { opis: '' };
  advokati;
  slucajevi;

  odabraniSlucaj;
  odabraniAdvokati;
  constructor(private _formBuilder: FormBuilder, private korsinikService: KorisnikService) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
      secondCtrl1: ['', Validators.required]
    });
    this.thridFormGroup = this._formBuilder.group({
      thridCtrl: ['', Validators.required]
    });
    this.korsinikService.getAllAdvokati().subscribe(res => {
      this.dataSource.data = res;
      this.advokati = res;
      console.log(res);
    });
    this.korsinikService.getAllSlucajForKorisnik().subscribe(res => {
      this.slucajevi = res;
      console.log(this.slucajevi)
    });
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: Advokat): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }
  save(){
  const slucajZaSlanje = { slucaj: this.odabraniSlucaj, odabraniAdvokati: this.selection.selected, Cenovniks: this.Cenovniks}
    
    console.log(slucajZaSlanje);
    this.korsinikService.postSlucajaSaAdvokatimaSaCenovnikom(slucajZaSlanje);
  }

}
