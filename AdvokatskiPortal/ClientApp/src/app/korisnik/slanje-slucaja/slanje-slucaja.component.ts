import { Majstor } from './../../model/Majstor';
import { AdvokatService } from './../../service/advokat.service';
import { Cenovnik } from './../../model/Cenovnik';
import { SlucajSlanjeVM } from './../../model/SlucajSlanjeVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';
import { Slucaj } from '../../model/Slucaj';
import { PrikazSlikaComponent } from '../dialog/prikaz-slika/prikaz-slika.component';
import { MatDialog, MatPaginator, MatSort, MatStepper } from '@angular/material';

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

  cenovnik = { vrstaPlacanja: '', kolicina: '' };
  vrstaPlacanja;
  displayedColumns: string[] = ['select', 'Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  selection = new SelectionModel<Majstor>(true, []);
  public dataSource = new MatTableDataSource<Majstor>();
  displayedColumnsSlucaj: string[] = ['Id', 'Naziv', 'Opis', 'Slike', 'Akcije'];
  public dataSourceSlucaj = new MatTableDataSource<Slucaj>();
  slucaj: Slucaj = new Slucaj();
  noviSlucaj: Slucaj = new Slucaj();
  slucajevi;
  panelStanje = false;
  SlucajVM: SlucajSlanjeVM = new SlucajSlanjeVM();
  nameFilter = new FormControl('');
  podKategorijaFilter = new FormControl('');

  filterTxt: string;
  // kategorije;
  odabraniSlucaj: Slucaj = new Slucaj();
  odabraniAdvokati;
  sviMajstori: any;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  constructor(private _formBuilder: FormBuilder, private korsinikService: KorisnikService, private advokatService: AdvokatService, public dialog: MatDialog) {
    this.dataSource.filterPredicate = this.tableFilter();
  }

  filterValues = {
    name: '',
    kategorijaFilter: '',
    podKategorijaFilter: ''
  };
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    // this.nameFilter.valueChanges
    //   .subscribe(
    //     name => {
    //       this.filterValues.name = name;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );
    this.korsinikService.getAllAdvokati().subscribe((res: any[]) => {
      this.sviMajstori = res;
    });
    // this.podKategorijaFilter.valueChanges
    //   .subscribe(
    //     id => {
    //       this.filterValues.podKategorijaFilter = id;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );

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

    this.korsinikService.getAllSlucajForKorisnik().subscribe((res: any) => {
      this.slucajevi = res;
      this.dataSourceSlucaj.data = this.slucajevi;
      this.remapImagesForDisplay(res) 
      console.log(this.slucajevi)
    });
  }

  getMajstoriBySelectedId() {
    const searchTerm = this.odabraniSlucaj.kategorijaId;
    this.dataSource.data = [...this.sviMajstori].filter(r => r.kategorije.some(k => k.kategorijaId === searchTerm));
  }
  tableFilter(): (data: any, filter: string) => boolean {
    const filterFunction = function (data, filter): boolean {
      const searchTerms = JSON.parse(filter);
      console.log(data)
      return (data.ime.toLowerCase().includes(searchTerms.name) || !searchTerms.name) &&
        data.majstorKategorijes.find(ca => ca.kategorijaId === <number>searchTerms.kategorijaFilter);
      // data.majstorKategorijes.find(ca => ca.kategorijaId === <number>this.odabraniSlucaj.kategorijaId);

    }
    return filterFunction;
  }
  openDialogPrikazSlika(element): void {
    const dialogRef = this.dialog.open(PrikazSlikaComponent, {
      width: '750px',
      height: '900px',
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
  acceptSlucaj(slucaj, stepper: MatStepper){
    this.odabraniSlucaj = slucaj;
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
  redirectToEdit(slucaj) {

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
  save() {
    this.SlucajVM.Majstors = this.selection.selected;
    this.SlucajVM.Slucaj = this.odabraniSlucaj;
    this.korsinikService.postSlucajaSaAdvokatimaSaCenovnikom(this.SlucajVM);
  }

  stepChanges(step) {
    if (step.selectedIndex === 1) {
      this.getMajstoriBySelectedId();
    }
  }

}
