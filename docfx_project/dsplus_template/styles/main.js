// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See LICENSE file in the project root for full license information.

window.addEventListener('DOMContentLoaded', () => {
  [...document.querySelectorAll('#toc .level1 > li')]
    .filter(e => e.children[1].title.startsWith('DSharpPlus'))
    .forEach(e => {
      e.parentElement.removeChild(e);
    })
});