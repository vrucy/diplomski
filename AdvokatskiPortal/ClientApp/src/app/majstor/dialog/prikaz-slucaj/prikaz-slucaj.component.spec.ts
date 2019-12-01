import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrikazSlucajComponent } from './prikaz-slucaj.component';

describe('PrikazSlucajComponent', () => {
  let component: PrikazSlucajComponent;
  let fixture: ComponentFixture<PrikazSlucajComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrikazSlucajComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrikazSlucajComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
