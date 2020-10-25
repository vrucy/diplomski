import { Component, OnInit, Input, ViewChild, NgZone } from '@angular/core';
import { Case } from '../../model/Case';
import { ActivatedRoute, Router } from '@angular/router'
import { UserService } from '../../service/User.service';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Picture } from '../../model/Picture';
import { delay } from 'rxjs/operators';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-edit-Case',
  templateUrl: './edit-Case.component.html',
  styleUrls: ['./edit-Case.component.css']
})
export class EditCaseComponent implements OnInit {
  Case;
  id;
  reader = new FileReader();
  private fileHandler: ImageHandler;
  Pictures: any[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService, private _ngZone: NgZone, private router: Router) { }
  @ViewChild('autosize', { static: false }) autosize: CdkTextareaAutosize;
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.userService.getCaseById(this.id).subscribe((res: any) => {
        this.Case = new Case();
        this.Case = res;
        this.remapImagesForDisplay(this.Case)
      });
    });
    console.log(this.id);
    this.reader.onload = this.handleReaderLoaded.bind(this);
  }
  private remapImagesForDisplay(data) {
    const basePictures = data.pictures.map(s => {
      s.pictureBytes = 'data:image/jpeg;base64,' + s.pictureBytes;
      return s;
    });
  }
  editCase() {
    this.Case.pictures.map((s) => {
      s.pictureBytes = s.pictureBytes.toString().split(',')[1];
      return s;

    })

    this.userService.EditCase(this.Case).subscribe((res: any) => {
      this.remapImagesForDisplay(res)
      this.Case = res;
      console.log(res)
      location.reload()
    }) 
    this.router.navigate(['/SendCase'])
  }
  previewPictures;
  deleteImage(img): string {
    const index: number = this.Case.pictures.indexOf(img);
    this.Case.pictures.splice(index, 1)
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
    const view = e.target.result;
    const _picture = this.fileHandler.ProcessFile(base64, view);
    console.log(this.Case.pictures)
    this.Pictures.push(_picture);
    
    this.Case.pictures.push(_picture);
    this.previewPictures = this.Case.pictures;
  }
}
class ImageHandler {
  private Picture: Picture;
  constructor(private file: File) {
    this.Picture = new Picture();
  }

  public ProcessFile(base64, show): Picture {
    this.handlePictureName();
    this.Picture.pictureBytes = 'data:image/png;base64,' + base64;
    this.Picture.show = show;
    return this.Picture;
  }
  private handlePictureName() {
    this.Picture.Name = this.file.name;
  }
}
