function setTheme(themeName) {
	localStorage.setItem('theme', themeName)
	document.documentElement.className = themeName
}

function toggleTheme() {
	const sliderRound = document.querySelector('.slider')
	sliderRound.classList.toggle('active')
	if (localStorage.getItem('theme') === 'theme-dark') {
		setTheme('theme-light')
	} else {
		setTheme('theme-dark')
	}
}

; (function () {
	if (localStorage.getItem('theme') === 'theme-dark') {
		setTheme('theme-dark')

	} else {
		setTheme('theme-light')
		document.querySelector('#switch').checked = "";
		document.querySelector('.slider').classList.remove("active");
	}
})()

const menuCollapse = document.querySelector('.menu__collapse')

function setMenuState(state) {
	localStorage.setItem('menuState', state)
}

function getMenuState() {
	return localStorage.getItem('menuState')
}

function initializeMenuState() {
	const savedState = getMenuState()
	if (savedState === 'closed') {
		document.querySelector('aside').classList.add('close')
	}
}

menuCollapse.addEventListener('click', () => {
	const aside = document.querySelector('aside')
	aside.classList.toggle('close')
	if (aside.classList.contains('close')) {
		setMenuState('closed')
	} else {
		setMenuState('open')
	}
})

initializeMenuState()



//const currentUrl = window.location.href;
//const menuLinks = document.querySelectorAll('.navbar ul li a');

//let isActiveSet = false;

//menuLinks.forEach(link => {
//	const href = link.getAttribute('href');
//	const listItem = link.parentElement;

//	// Проверяем, соответствует ли текущая страница домашней странице
//	const isHome = currentUrl.endsWith('/');
//	console.log(isHome)

//	if ((currentUrl.includes(href) || isHome) ) {
//		const icon = listItem.querySelector('.icon');
//		if (icon) {
//			icon.classList.add('active');
//		}
//		isActiveSet = true; // Устанавливаем флаг, что активный класс уже был установлен
//	}
//});


//function initializeModal(
//	showBtnSelector,
//	maskModalSelector,
//	modalSelector,
//	closeBtnSelector
//) {
//	const showBtns = document.querySelectorAll(showBtnSelector)
//	const maskModal = document.querySelector(maskModalSelector)
//	const modals = document.querySelectorAll(modalSelector)
//	const closeBtns = document.querySelectorAll(closeBtnSelector)

//	function openModal(index) {
//		maskModal.classList.add('active')
//		modals[index].classList.add('modal-active')
//	}

//	function closeModal(index) {
//		if (modals[index]) {
//			maskModal.classList.remove('active')
//			modals[index].classList.remove('modal-active')
//		} else {
//			console.error('Modal not found at index', index)
//		}
//	}

//	showBtns.forEach((btn, index) => {
//		btn.addEventListener('click', () => openModal(index))
//	})

//	closeBtns.forEach((btn, index) => {
//		btn.addEventListener('click', () => {
//			const modalIndex = Array.from(modals).indexOf(btn.closest('.modal'))
//			closeModal(modalIndex)
//		})
//	})

//	maskModal.addEventListener('click', () => {
//		closeModal(
//			Array.from(modals).findIndex(modal =>
//				modal.classList.contains('modal-active')
//			)
//		)
//	})
//}

//initializeModal('.show-modal', '.mask-modal', '.modal', '.close')

//document.addEventListener('keyup', e => {
//	if (e.code == 'Escape') closeModal()
//})

const nestedTabSelect = (tabsElement, currentElement) => {
	const tabs = tabsElement ?? 'ul.tabs'
	const currentClass = currentElement ?? 'active'

	document.querySelectorAll(tabs).forEach(function (tabContainer) {
		let activeLink, activeContent
		const links = Array.from(tabContainer.querySelectorAll('a'))

		activeLink =
			links.find(function (link) {
				return link.getAttribute('href') === location.hash
			}) || links[0]
		activeLink.classList.add(currentClass)

		activeContent = document.querySelector(activeLink.getAttribute('href'))
		activeContent.classList.add(currentClass)

		links.forEach(function (link) {
			if (link !== activeLink) {
				const content = document.querySelector(link.getAttribute('href'))
				content.classList.remove(currentClass)
			}
		})

		tabContainer.addEventListener('click', function (e) {
			if (e.target.tagName === 'A') {
				// Make the old tab inactive.
				activeLink.classList.remove(currentClass)
				activeContent.classList.remove(currentClass)

				// Update the variables with the new link and content.
				activeLink = e.target
				activeContent = document.querySelector(activeLink.getAttribute('href'))

				// Make the tab active.
				activeLink.classList.add(currentClass)
				activeContent.classList.add(currentClass)

				e.preventDefault()
			}
		})
	})
}

nestedTabSelect('ul.tabs', 'active')