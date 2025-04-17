import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {

  appName = 'Code Pulse';
  version = '1.0.0';
  teamMembers = ['Guillaume', 'Louis', 'Chloé', 'Léa', 'Mélanie', 'Nicolas', 'Julien', 'Maxime'];
  createdDate = 'Avril 2025';

}
