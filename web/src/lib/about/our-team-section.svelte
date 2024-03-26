<script>
	const ourTeamImgModules = import.meta.glob('$lib/assets/about/our-team/*.jpg', {
		eager: true,
		import: 'default'
	});

	const ourTeamImagesPath = Object.keys(ourTeamImgModules)[0].split('/').slice(0, -1).join('/');

	const teamMembers = [
		{ name: 'Lawson Arnold', image: 'person_1.jpg' },
		{ name: 'Jeremy Walker', image: 'person_2.jpg' },
		{ name: 'Patrik White', image: 'person_3.jpg' },
		{ name: 'Kathryn Ryan', image: 'person_4.jpg' }
	];
</script>

<div class="py-5">
	<div class="container">
		<div class="row mb-5">
			<div class="col-lg-5 mx-auto text-center">
				<h2 class="section-title">Our Team</h2>
			</div>
		</div>

		<div class="row">
			{#each teamMembers as member}
				<div class="col-12 col-md-6 col-lg-3 mb-5 mb-md-0">
					<img
						src={ourTeamImgModules[`${ourTeamImagesPath}/${member.image}`] + ''}
						alt={member.image}
						class="img-fluid mb-5" />
					<a href={'javascript:;'} class="h3">{member.name}</a>
					<span class="d-block position mb-4 mt-3 text-secondary fs-5 fw-medium"
						>CEO, Founder, Atty.</span>
					<p class="text-secondary">
						Separated they live in. Separated they live in Bookmarksgrove right at the coast of the
						Semantics, a large language ocean.
					</p>
					<p class="mb-0">
						<a href={'#'} class="text-info">
							Learn More <i class="fa-solid fa-arrow-right ms-1"></i>
						</a>
					</p>
				</div>
			{/each}
		</div>
	</div>
</div>

<style lang="scss">
	a.h3:hover,
	a.text-info:hover {
		text-decoration: none;
	}
</style>
