import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Pipe({
  name: 'bbcode'
})
export class BbcodePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(value: string): SafeHtml {
    if (!value) {
      return '';
    }

    // Define the BBCode tags and their corresponding HTML tags
    const bbcodeReplacements = [
      [/\[b\](.*?)\[\/b\]/gi, '<strong>$1</strong>'],
      [/\[i\](.*?)\[\/i\]/gi, '<em>$1</em>'],
      [/\[u\](.*?)\[\/u\]/gi, '<u>$1</u>'],
      [/\[s\](.*?)\[\/s\]/gi, '<strike>$1</strike>'],
      [/\[br\]/gi, '<br>'],
      [/\[h1\](.*?)\[\/h1\]/gi, '<h1>$1</h1>'],
      [/\[h2\](.*?)\[\/h2\]/gi, '<h2>$1</h2>'],
      [/\[h3\](.*?)\[\/h3\]/gi, '<h3>$1</h3>'],
      [/\[h4\](.*?)\[\/h4\]/gi, '<h4>$1</h4>'],
      [/\[h5\](.*?)\[\/h5\]/gi, '<h5>$1</h5>'],
      [/\[h6\](.*?)\[\/h6\]/gi, '<h6>$1</h6>'],
      [/\[url=(.*?)\](.*?)\[\/url\]/gi, '<a href="$1" target="_blank" rel="noopener noreferrer">$2</a>'],
      [/\[img\](.*?)\[\/img\]/gi, '<img src="$1" alt="Image"/>'],
      [/\[b\](.*?)\[\/b\]/gi, '<strong>$1</strong>'],
      [/\[i\](.*?)\[\/i\]/gi, '<em>$1</em>'],
      [/\[u\](.*?)\[\/u\]/gi, '<u>$1</u>'],
      [/\[s\](.*?)\[\/s\]/gi, '<strike>$1</strike>'],
      [/\[url:(.*?)\](.*?)\[\/url\]/gi, '<a href="$1" target="_blank" rel="noopener noreferrer">$2</a>'],
      [/\[img\](.*?)\[\/img\]/gi, '<img src="$1" alt="Image"/>'],
      [/\[color=(.*?)\](.*?)\[\/color\]/gi, '<span style="color: $1;">$2</span>'],
      [/\[size=(.*?)\](.*?)\[\/size\]/gi, '<span style="font-size: $1px;">$2</span>'],
      [/\[quote\](.*?)\[\/quote\]/gi, '<blockquote>$1</blockquote>'],
      [/\[quote=(.*?)\](.*?)\[\/quote\]/gi, '<blockquote><strong>$1:</strong><br>$2</blockquote>'],
      [/\[code\](.*?)\[\/code\]/gi, '<pre><code>$1</code></pre>'],
      [/\[pre\](.*?)\[\/pre\]/gi, '<pre>$1</pre>'],
      [/\[ul\](.*?)\[\/ul\]/gis, '<ul>$1</ul>'],
      [/\[ol\](.*?)\[\/ol\]/gis, '<ol>$1</ol>'],
      [/\[li\](.*?)\[\/li\]/gi, '<li>$1</li>'],
      [/\[table\](.*?)\[\/table\]/gis, '<table>$1</table>'],
      [/\[tr\](.*?)\[\/tr\]/gis, '<tr>$1</tr>'],
      [/\[td\](.*?)\[\/td\]/gi, '<td>$1</td>'],
      [/\[th\](.*?)\[\/th\]/gi, '<th>$1</th>'],
      [/\[center\](.*?)\[\/center\]/gi, '<div style="text-align: center;">$1</div>'],
      [/\[left\](.*?)\[\/left\]/gi, '<div style="text-align: left;">$1</div>'],
      [/\[right\](.*?)\[\/right\]/gi, '<div style="text-align: right;">$1</div>'],
      [/\[justify\](.*?)\[\/justify\]/gi, '<div style="text-align: justify;">$1</div>'],
      [/\[youtube\]https?:\/\/www\.youtube\.com\/watch\?v=([A-Za-z0-9\-_]+)(?:.*?)\[\/youtube\]/gi, '<iframe width="560" height="315" src="https://www.youtube.com/embed/$1" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>'],
      [/\[quote\](.*?)\[\/quote\]/gis, '<blockquote>$1</blockquote>'],
      [/\[small\](.*?)\[\/small\]/gi, '<small><p>$1</p></small>'],
      [/\[img:(.*?)\|size\|(.*?)\]/gi, '<img src="$1" alt="Image" style="width: $2px;">'],
      [/\[img:(.*?)\]/gi, '<img src="$1" alt="Image">'],
      [/\[col\](.*?)\[\/col\]/gis, '<div class="col-md-6">$1</div>'],
      [/\[row\](.*?)\[\/row\]/gis, '<div class="row">$1</div>'],
      // Add more rules as needed

      // Not technically BBCode, but will convert newlines to proper spaces
      [/\r\n/g, '\n'],
      [/(\r\n){2}/g, '\n']
    ];

    // Replace the BBCode tags with their HTML counterparts
    for (const [regex, replacement] of bbcodeReplacements) {
      value = value.replace(regex, replacement.toString());
    }

    // Sanitize the resulting HTML and return it as a SafeHtml object
    return this.sanitizer.bypassSecurityTrustHtml(value);
  }
}
