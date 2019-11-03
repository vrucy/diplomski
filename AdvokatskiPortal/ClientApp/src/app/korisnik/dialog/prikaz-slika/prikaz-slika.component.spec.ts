import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrikazSlikaComponent } from './prikaz-slika.component';

describe('PrikazSlikaComponent', () => {
  let component: PrikazSlikaComponent;
  let fixture: ComponentFixture<PrikazSlikaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrikazSlikaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrikazSlikaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
