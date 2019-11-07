import { Majstor } from './../../model/Majstor';
import { AdvokatService } from './../../service/advokat.service';
import { Cenovnik } from './../../model/Cenovnik';
import { SlucajSlanjeVM } from './../../model/SlucajSlanjeVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
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
  isLinear = true;
  filterTxt: string;
  // kategorije;
  odabraniSlucaj: Slucaj = new Slucaj();
  odabraniAdvokati;
  sviMajstori: any;
  // odabirMajstoraFormGroup: FormGroup;
  // odbairSlucajaFormGruop: FormGroup;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private _formBuilder: FormBuilder, private korsinikService: KorisnikService,
    private advokatService: AdvokatService, public dialog: MatDialog) {
    // this.dataSource.filterPredicate = this.tableFilter();
  }

  // filterValues = {
  //   name: '',
  //   kategorijaFilter: '',
  //   podKategorijaFilter: ''
  // };
  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = new FormGroup({

    });
    // this.odbairSlucajaFormGruop = new FormGroup({
    //   odabirSlucja: this._formBuilder.group({
    //     odabirSlucaja1: this._formBuilder.array()
    //   });
    // });
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

    this.korsinikService.getAllSlucajForKorisnik().subscribe((res: any) => {
      this.slucajevi = res;
      this.dataSourceSlucaj.data = this.slucajevi;
      this.remapImagesForDisplay(res)
      console.log(this.slucajevi)
    });
  }
  // private mapToCheckboxArrayGroup(data: string[]): FormArray {
  //   return this._formBuilder.array(data.map((i) => {
  //     return this._formBuilder.group({
  //       name: i,
  //       selected: false
  //     });
  //   }));
  // }
  // getMajstorFormGroup(): FormGroup {
  //   return this._formBuilder.group({
  //     size: [size],
  //     toppings: this.mapToCheckboxArrayGroup(this.sviMajstori)
  //   }, {
  //     validator: ['', Validators.required]
  //   });
  // }
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
    slucaj.slike.forEach((slika: any) => {
      if (slika.slikaProp) {
        const base64result = slika.slikaProp.split(',')[1];
        slika.slikaProp = base64result;
      }
    });
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
