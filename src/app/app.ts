import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { Loading } from './shared/loading/loading';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, MenubarModule,Loading],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'e-commerce-primeng';
}
