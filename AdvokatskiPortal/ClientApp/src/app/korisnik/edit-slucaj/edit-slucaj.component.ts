import { Component, OnInit, Input, ViewChild, NgZone } from '@angular/core';
import { Slucaj } from '../../model/Slucaj';
import { ActivatedRoute, Router } from '@angular/router'
import { KorisnikService } from '../../service/korisnik.service';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Slika } from '../../model/Slika';
import { delay } from 'rxjs/operators';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-edit-slucaj',
  templateUrl: './edit-slucaj.component.html',
  styleUrls: ['./edit-slucaj.component.css']
})
export class EditSlucajComponent implements OnInit {
  slucaj: Slucaj;
  id;
  reader = new FileReader();
  private fileHandler: ImageHandler;
  slike: Slika[] = [];

  constructor(private route: ActivatedRoute, private korisnikService: KorisnikService, private _ngZone: NgZone, private router: Router) { }
  @ViewChild('autosize', { static: false }) autosize: CdkTextareaAutosize;
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.korisnikService.getSlucajById(this.id).subscribe((res: any) => {
        this.slucaj = new Slucaj();
        this.slucaj = res;
        this.remapImagesForDisplay(this.slucaj)
      });
    });
    console.log(this.id);
    this.reader.onload = this.handleReaderLoaded.bind(this);
  }
  private remapImagesForDisplay(data) {
    const baseSlike = data.slike.map(s => {
      s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
      console.log(s)
      return s;
    });
    // this.images = data.slike.map((s) => s.slikaProp);
  }
  editSlucaj() {
    this.slucaj.slike.map((s) => {
      s.slikaProp = s.slikaProp.toString().split(',')[1];
      return s;

    })

    this.korisnikService.editSlucaj(this.slucaj).subscribe((res: any) => {
      this.remapImagesForDisplay(res)
      this.slucaj = res;
      console.log(res)
      location.reload()
    }) 
    this.router.navigate(['/slanjeSlucaja'])
  }
  prikazSlike;
  deleteImage(img): string {
    const index: number = this.slucaj.slike.indexOf(img);
    this.slucaj.slike.splice(index, 1)
    console.log('slika: ', this.slucaj.slike)

    return img;
  }
  onUploadChange(evt: any) {
    const file = evt.target.files[0];
    if (file) {
      this.fileHandler = new ImageHandler(file);
      this.reader.readAsDataURL(file);
    }
  }
  handleReaderLoaded(e) {
    const base64 = e.target.result.toString().split(',')[1];
    console.log(base64)
    const prikaz = e.target.result;
    const slika = this.fileHandler.ProcessFile(base64, prikaz);
    this.slike.push(slika);
    this.slucaj.slike.push(slika);
    this.prikazSlike = this.slucaj.slike;
  }
}
class ImageHandler {
  private slika: Slika;
  constructor(private file: File) {
    this.slika = new Slika();
  }

  public ProcessFile(base64, prikaz): Slika {
    this.handlePictureName();
    this.slika.slikaProp = 'data:image/png;base64,' + base64;
    this.slika.prikaz = prikaz;
    return this.slika;
  }
  private handlePictureName() {
    this.slika.Naziv = this.file.name;
  }
}
