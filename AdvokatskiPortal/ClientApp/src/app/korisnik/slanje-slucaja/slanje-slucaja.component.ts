import { Majstor } from './../../model/Majstor';
import { MajstorService } from './../../service/majstor.service';
import {ChangeDetectorRef } from '@angular/core';
import { SlucajSlanjeVM } from './../../model/SlucajSlanjeVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';
import { Slucaj } from '../../model/Slucaj';
import { PrikazSlikaComponent } from '../dialog/prikaz-slika/prikaz-slika.component';
import { MatDialog, MatPaginator, MatSort, MatStepper, MatSnackBar } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { UspesnoStePoslaliSlucajComponent } from '../../snackBar/uspesno-ste-poslali-slucaj/uspesno-ste-poslali-slucaj.component';

@Component({
  selector: 'app-slanje-slucaja',
  templateUrl: './slanje-slucaja.component.html',
  styleUrls: ['./slanje-slucaja.component.css']
})
export class SlanjeSlucajaComponent implements OnInit {
  // cenovnik = { vrstaPlacanja: '', kolicina: '' };
  // vrstaPlacanja;
  displayedColumns: string[] = ['select',  'ime', 'prezime', 'mesto', 'ulica', 'email'];
  selection = new SelectionModel<Majstor>(true, []);
  public dataSource = new MatTableDataSource<Majstor>();
  displayedColumnsSlucaj: string[] = [ 'naziv', 'opis', 'slike', 'akcije'];
  public dataSourceSlucaj = new MatTableDataSource<Slucaj>();
  slucaj: Slucaj = new Slucaj();
  noviSlucaj: Slucaj = new Slucaj();
  slucajevi;
  // panelStanje = false;
  SlucajVM: SlucajSlanjeVM = new SlucajSlanjeVM();
  nameFilter = new FormControl('');
  podKategorijaFilter = new FormControl('');
  isLinear = true;
  filterTxt: string;
  odabraniSlucaj: Slucaj = new Slucaj();
  // odabraniMajstori;
  sviMajstori: any;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  @ViewChild('sort', { static: true }) sort: MatSort;
  // @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild('sort1', { static: true }) sort1: MatSort;
  // @ViewChild(MatPaginator, { static: true }) paginator1: MatPaginator;
  @ViewChild('paginator', { read: MatPaginator, static: true}) paginator: MatPaginator;
  @ViewChild('paginator1', { read: MatPaginator, static: true } ) paginator1: MatPaginator;
  constructor(private _formBuilder: FormBuilder, private korsinikService: KorisnikService,
    private router: Router, public dialog: MatDialog, private cdref: ChangeDetectorRef, private _snackBar: MatSnackBar) {
    // this.dataSource.filterPredicate = this.tableFilter();
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  ngOnInit() {
    this.dataSource.paginator = this.paginator1;
    this.dataSource.sort = this.sort1;
    this.dataSourceSlucaj.sort = this.sort;
    this.dataSourceSlucaj.paginator = this.paginator;
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      items: [ '']
    });
    this.korsinikService.getAllMajstori().subscribe((res: any) => {
      this.sviMajstori = res;
    });

    this.korsinikService.getAllSlucajForKorisnik().subscribe((res: any) => {
      this.slucajevi = res;
      this.dataSourceSlucaj.data = this.slucajevi;
      this.remapImagesForDisplay(res);
    });
  }
  getMajstoriBySelectedId() {
    const searchTerm = this.odabraniSlucaj.kategorijaId;
    this.dataSource.data = [...this.sviMajstori].filter(r => r.kategorije.some(k => k.kategorijaId === searchTerm));
  }
  openDialogPrikazSlika(element): void {
    const dialogRef = this.dialog.open(PrikazSlikaComponent, {
      width: '40%',
      height: '80%',
      data: { slike: element.slike }
    });

  }
  private remapImagesForDisplay(data) {
    data.forEach(slucaj => {
      const baseSlike = slucaj.slike.map(s => {
        s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
        return s;
      });
    });
  }

  acceptSlucaj(slucaj, stepper: MatStepper) {
    console.log(slucaj)
    slucaj.slike.forEach((slika: any) => {
      if (slika.slikaProp) {
        const base64result = slika.slikaProp.split(',')[1];
        slika.slikaProp = base64result;
      }
    });
    this.odabraniSlucaj = slucaj;
    this.firstFormGroup.get('firstCtrl').setValue('true') ;
    stepper.next();
  }
  resetFilter() {
    this.nameFilter.reset();
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

  checkboxLabel(row?: Majstor): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }
  resetStepper(stepper) {
    console.log(this.selection)
    stepper.reset();
    this.selection.clear();
    console.log(this.odabraniSlucaj)
     this.dataSource.data = null;
     this.odabraniSlucaj = null;
    console.log(this.odabraniSlucaj)
  }
  onSubmit(){

  }
  validateForm(slucaj, stepper: MatStepper){
    // this.firstFormGroup.get('firstCtrl').value = 'ture';
    this.firstFormGroup.get('firstCtrl').setValue('true') ;
    slucaj.slike.forEach((slika: any) => {
      if (slika.slikaProp) {
        const base64result = slika.slikaProp.split(',')[1];
        slika.slikaProp = base64result;
      }
    });
    this.odabraniSlucaj = slucaj;
    stepper.next();


  }
  save() {
    this.SlucajVM.Majstors = this.selection.selected;
    this.SlucajVM.Slucaj = this.odabraniSlucaj;
    this.korsinikService.postSlucajMajstorima(this.SlucajVM).subscribe(res => {
      this._snackBar.openFromComponent(UspesnoStePoslaliSlucajComponent, {
        duration: 3000
      });
    });
  }

  stepChanges(step) {
    if (step.selectedIndex === 1) {
      this.getMajstoriBySelectedId();
    }
  }

}
