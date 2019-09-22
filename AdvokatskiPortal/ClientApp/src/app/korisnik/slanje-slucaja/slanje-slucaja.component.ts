import { Cenovnik } from './../../model/Cenovnik';
import { SlucajSlanjeVM } from './../../model/SlucajSlanjeVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { Advokat } from '../../model/Advokat';
import { MatTableDataSource } from '@angular/material/table';
import { Slucaj } from '../../model/Slucaj';

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

  cenovnik = {vrstaPlacanja: '', kolicina: ''};
  vrstaPlacanja;
  displayedColumns: string[] = ['select', 'Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  selection = new SelectionModel<Advokat>(true, []);
  public dataSource = new MatTableDataSource<Advokat>();
  slucaj = { opis: '' };
  // noviSlucaj = { opis: ''};
  noviSlucaj : Slucaj = new Slucaj();
  advokati;
  slucajevi;
  panelStanje = false;
  SlucajVM: SlucajSlanjeVM = new SlucajSlanjeVM();

  filterTxt: string;
  odabraniSlucaj: Slucaj = new Slucaj();
  odabraniAdvokati;
  constructor(private _formBuilder: FormBuilder, private korsinikService: KorisnikService) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
      noviSlucajNaziv: [''],
      noviSlucajOpis: ['']
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
    });
    this.korsinikService.getAllSlucajForKorisnik().subscribe(res => {
      this.slucajevi = res;
      console.log(this.slucajevi)
    });
  }
  // PROBLEM KAD SE KREIRA NE UPISE SE AUTUTOMATSKI  U SELECT PROBLEM JE U ngLifeCiCLES
  kreiranjeSlucaja(){
    this.korsinikService.kreiranjeSlucaja(this.noviSlucaj);
    console.log(this.noviSlucaj)
  }
  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
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
  save() {

    this.SlucajVM.Advokats =  this.selection.selected;

    this.SlucajVM.Slucaj = this.odabraniSlucaj;

    const c: Cenovnik = new Cenovnik();
    c.kolicina = this.cenovnik.kolicina;
    c.vrstaPlacanja = this.cenovnik.vrstaPlacanja;
    this.SlucajVM.Cenovniks =  [c];
    this.korsinikService.postSlucajaSaAdvokatimaSaCenovnikom(this.SlucajVM);
  }

}
