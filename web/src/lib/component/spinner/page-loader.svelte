<script>
	import BeanEaterSVG from '$lib/assets/images/Bean Eater-1s-200px.svg';
</script>

<div class="modal fade modal-background-custom show" {...$$restProps}>
	<div class="modal-dialog modal-dialog-centered">
		<div class="w-100 text-center">
			<div>
				<img src={BeanEaterSVG} alt="Loading..." />
			</div>
			<span>Loading your page. Please be patient</span>
		</div>
	</div>
</div>

<style>
	.modal-background-custom {
		background: rgba(255, 255, 255, 1);
	}

	.show {
		display: block;
	}
</style>
