<script>
	import { showSuccess } from '$lib/component/toastr.svelte';

	function onSubmit() {
		showSuccess('Your message has been reached to us');
	}
</script>

<div class="container mb-5 mt-1">
	<div class="row">
		<div class="col-lg-3 col-md-3 col-sm-6 text-center">
			<i class="fa-solid fa-phone h3 text-secondary"></i>
			<h4 class="fw-bold">Phone</h4>
			<p class="fw-medium">+1 (999) 999-999</p>
		</div>
		<div class="col-lg-3 col-md-3 col-sm-6 text-center">
			<i class="fa-solid fa-location-dot h3 text-secondary"></i>
			<h4 class="fw-bold">Address</h4>
			<p class="fw-medium">199 Avenue S, Lubbock, Texas 79415, USA</p>
		</div>
		<div class="col-lg-3 col-md-3 col-sm-6 text-center">
			<i class="fa-regular fa-clock h3 text-secondary"></i>
			<h4 class="fw-bold">Open time</h4>
			<p class="fw-medium">10:00 am to 23:00 pm</p>
		</div>
		<div class="col-lg-3 col-md-3 col-sm-6 text-center">
			<i class="fa-regular fa-envelope h3 text-secondary"></i>
			<h4 class="fw-bold">Email</h4>
			<p class="fw-medium">info@example.com</p>
		</div>
	</div>
</div>

<div class="my-5">
	<iframe
		class="w-100"
		src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3323.356430445871!2d-101.8624006172556!3d33.59605527293671!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x86fe12c4f4115643%3A0x4be2acd0830de582!2s199%20Avenue%20S%2C%20Lubbock%2C%20TX%2079415%2C%20Hoa%20K%E1%BB%B3!5e0!3m2!1svi!2s!4v1701750814877!5m2!1svi!2s"
		style="border:0; min-height: 450px;"
		loading="lazy"
		referrerpolicy="no-referrer-when-downgrade"
		title="199 Heffernan Avenue, Texas, 77218, United States"></iframe>
</div>

<div class="container my-5">
	<div class="w-50 mx-auto p-2">
		<div class="mb-5 mt-5 text-center">
			<h2 class="fw-bold mb-3">Have you any question?</h2>
			<p class="fw-medium">
				Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur, ratione! Laboriosam est,
				assumenda. Perferendis, quo alias quaerat aliquid. Corporis ipsum minus voluptate? Dolore,
				esse natus!
			</p>
		</div>
		<form action="#" on:submit={onSubmit}>
			<div class="form-floating mb-3">
				<input type="text" class="form-control" id="fullName" placeholder="John Doe" />
				<label for="fullName">Your name</label>
			</div>
			<div class="form-floating mb-3">
				<input type="text" class="form-control" id="email" placeholder="john.doe@gmail.com" />
				<label for="email">Your email</label>
			</div>
			<div class="form-floating mb-3">
				<input type="text" class="form-control" id="subject" placeholder="Product broken" />
				<label for="subject">Subject</label>
			</div>
			<div class="form-floating">
				<textarea
					class="form-control"
					placeholder="Leave a comment here"
					id="message"
					style="height: 200px;"></textarea>
				<label for="message">Message</label>
			</div>
			<div class="my-3 d-grid">
				<button class="btn btn-outline-primary rounded-4 fw-semibold fs-4 py-2">Send</button>
			</div>
		</form>
	</div>
</div>
